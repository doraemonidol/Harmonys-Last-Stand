using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNS_USE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

namespace Animmal.NotificationSystem
{
    //To pool each variation of notification items. 
    public class NotificationItemList
    {
        public List<NotificationStatus> Variation = new List<NotificationStatus>();
    }

#if UNS_USE_ADDRESSABLES
    [System.Serializable]
    public class NotificationItemAddressableField
    {
        public class UnityNotificationDataEvent : UnityEvent<NotificationStatus> { }
        public UnityNotificationDataEvent OnLoadingComplete = new UnityNotificationDataEvent();
        public AssetReference AddressableNotificationItemPrefab;
        public NotificationItem LoadedItem { get; private set; }
        bool IsLoading;
        AsyncOperationHandle<GameObject> Handle;
        
        public IEnumerator LoadPrefabAndWait(NotificationDisplay _Display, NotificationStatus _Status)
        {
            if (LoadedItem == null)
            {
                if (IsLoading)
                {
                    while (IsLoading)
                    {
                        yield return null;
                    }
                    OnLoadingComplete.Invoke(_Status);
                }
                else
                {
                    IsLoading = true;
                    Handle = Addressables.LoadAssetAsync<GameObject>(AddressableNotificationItemPrefab);
                    yield return Handle;
                    if (Handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        LoadedItem = Handle.Result.GetComponent<NotificationItem>();
                    }
                    else
                    {
                        Debug.LogError("Ultimate Notification System: Addresable Prefab not assigned on Object: " + _Display.gameObject.name + " for Notification Display " + _Display.UniqueID);
                    }
                    IsLoading = false;
                    OnLoadingComplete.Invoke(_Status);
                }
            }
            else
            {
                OnLoadingComplete.Invoke(_Status);
            }
            yield break;
        }

        public void UnloadItem()
        {
            if (Handle.IsValid() && Handle.IsDone && LoadedItem != null)
            {
                LoadedItem = null;
                Addressables.Release<GameObject>(Handle);
            }
        }
    }
#endif

    public enum NotificationSpawnDirection { Top, Bottom}

    [AddComponentMenu("Animmal/NotificationSystem/Notification Display")]
    /// <summary>
    /// Notification Display is responsible for showing and managing notification items
    /// </summary>
    public partial class NotificationDisplay : MonoBehaviour, IDisplayable
    {
#region VARIABLES
        
        [Header("Notification Manager")]
        [Tooltip("If you use NotificationManager, this ID will be used to identify the display")]
        public string UniqueID;
        [Tooltip("This will determine if Notification Display will register itself to be available to Notification Manager.")]
        public bool RegisterDisplayWithNotificationManager = true;

        [Header("Notification Item Setup")]
#if UNS_USE_ADDRESSABLES
        [Tooltip("Addressable Versions Will Be Favored if assigned. Notification item variations that can be spawned by this display. EX: Positive, Neutral, Negative alert.")]
        public List<NotificationItemAddressableField> AddressableNotificationItemPrefabs = new List<NotificationItemAddressableField>();
        [Tooltip("Should notification addressable everytime notification is hidden or only when display is destroyed? Does not work when pooling is enabled")]
        public bool UnloadAddressableWhenNotificationIsHidden;
#endif
        [Tooltip("Notification item variations that can be spawned by this display. EX: Positive, Neutral, Negative alert.")]
        public List<NotificationItem> NotificationItemPrefabs = new List<NotificationItem>();
        [Tooltip("Parent object for spawning notification items.")]
        public Transform NotificationItemSpawnParent;
        [Tooltip("Spawn at the top of the hierarchy list or at the end.")]
        public NotificationSpawnDirection SpawnDirection;

        [Header("Retrigger Behavior")]
        [Tooltip("You can enable/disable queuing behavior altogether.")]
        public bool QueuingEnabled = true;
        [Tooltip("How many notifications can be shown at once. Negative Value (-1) = Unlimited.")]
        public int MaxNotifications = -1;
        [Tooltip("How long to wait before showing next notification in queue.")]
        [Min(0)]
        public float NotificationCooldownTime = 1f;

        [Header("Auto Advance")]
        [Tooltip("When a new notification is shown and there is no room for new notifications. First notification item will be hidden to make room for new one.")]
        public bool AutoAdvanceQueue = true;
        [Tooltip("If AutoAdvanceQueue is turned on, should the first item be hidden instantly?.")]
        public bool AutoAdvanceQueueInstantHide = true;

        [Header("Pooling")]
        [Tooltip("Recycle notifications?")]
        public bool UsePooling = true;
        [Tooltip("If pooling is used disable GameObject of Notification item when it has finished hiding?")]
        public bool DisableGameObjectOnHiddenItems;

        [Header("Data Processors")]
        [Tooltip("Add Objects here that have monobehavior component with INotificationDataProcessor interface to do global data processing on all Notification Data passed through this display")]
        public List<GameObject> DataProcessorGameobjects = new List<GameObject>();

        [Header("Events")]
        [Tooltip("Called when notification item is spawned or recycled to be shown")]
        public UnityNotificationItemEvent OnItemShown = new UnityNotificationItemEvent();
        [Tooltip("Called when notification item finished hiding")]
        public UnityNotificationItemEvent OnItemHidden = new UnityNotificationItemEvent();
        [Tooltip("Called when notification display recieves show command.")]
        public UnityBoolEvent OnDisplayShow = new UnityBoolEvent();
        [Tooltip("Called when notification display recieves hide command.")]
        public UnityBoolEvent OnDisplayHide = new UnityBoolEvent();

        //tracking display state
        public bool IsDisabled { get; set; }
        public bool IsPaused { get; set; }

        //Implementing INotificationDisplayable Interface 
        public virtual UnityBoolEvent OnShowEvent { get { return OnDisplayShow; } }
        public virtual UnityBoolEvent OnHideEvent { get { return OnDisplayHide; } }

        //pooling 
        protected List<NotificationItemList> PooledNotificationItems = new List<NotificationItemList>();
        
        //Active item handling 
        protected List<NotificationStatus> ActiveNotificationItems = new List<NotificationStatus>();
        protected List<NotificationStatus> QueuedNotifications = new List<NotificationStatus>();
        protected IEnumerator RetriggerCoroutine;
        protected float LastNotificationTime = -1;

        //Data processors 
        protected List<INotificationDataProcessor> DataProcessors = new List<INotificationDataProcessor>();

        //Init
        bool InitComplete = false;

#endregion

#region UNITYFUNCTIONS

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void OnEnable()
        {
            //See if we had anything queued to manage 
            if (QueuedNotifications.Count > 0)
            {
                RetriggerCoroutine = NotificationRetriggerCheckDelayed(NotificationCooldownTime);
                StartCoroutine(RetriggerCoroutine);
            }
        }

        protected virtual void OnDisable()
        {
            if (RetriggerCoroutine != null)
                StopCoroutine(RetriggerCoroutine);
        }

        protected virtual void OnDestroy()
        {
            UnRegisterDisplay();
#if UNS_USE_ADDRESSABLES
            UnloadAddressables();
#endif

        }

#endregion

#region INIT
        protected virtual void Init()
        {
            if (InitComplete)
                return;
            //Make sure we have at least something for ID
            if (string.IsNullOrEmpty(UniqueID))
                UniqueID = Guid.NewGuid().ToString();

            //For use with Notification Manager
            if (RegisterDisplayWithNotificationManager)
                RegisterDisplay();

            //Setup pooling lists needed to track item variations 
            if (UsePooling)
            {
#if UNS_USE_ADDRESSABLES
          
                for (int i = 0; i < AddressableNotificationItemPrefabs.Count; i++)
                {
                    PooledNotificationItems.Add(new NotificationItemList());
                }
                for (int i = AddressableNotificationItemPrefabs.Count; i < NotificationItemPrefabs.Count; i++)
                {
                    PooledNotificationItems.Add(new NotificationItemList());
                }
#else
                for (int i = 0; i < NotificationItemPrefabs.Count; i++)
                {
                    PooledNotificationItems.Add(new NotificationItemList());
                }
#endif
            }
            //Populate data processors with components from supplied gameobjects
            InitializeDataProcessors();
            InitComplete = true;
        }

        protected virtual void InitializeDataProcessors()
        {
            for (int i = 0; i < DataProcessorGameobjects.Count; i++)
            {
                if (DataProcessorGameobjects[i] == null)
                {
                    Debug.LogError("Ultimate Notification System - Notification Display: "+ gameObject.name +" No gameobject assigned to DataProcessorGameobjects list at index " + i.ToString());
                    continue; // Skip this loop iteration 
                }

                INotificationDataProcessor _Data = DataProcessorGameobjects[i].GetComponent<INotificationDataProcessor>();
                if (_Data == null)
                {
                    Debug.LogError("Ultimate Notification System - Notification Manager: " + gameObject.name + " No INotificationDataProcessor Component found on gameobject " + DataProcessorGameobjects[i].name);
                    continue; // Skip this loop iteration 
                }

                DataProcessors.Add(_Data);
            }
        }
#endregion

#region DISPLAY_REGISTRATION

        protected virtual void RegisterDisplay()
        {
            if (NotificationManager.Instance != null)
                NotificationManager.Instance.RegisterDisplay(UniqueID, this);
        }

        protected virtual void UnRegisterDisplay()
        {
            if (NotificationManager.Instance != null)
                NotificationManager.Instance.UnRegisterDisplay(UniqueID);
        }

#endregion

#region DISPLAYVISIBILITY
        public virtual void ShowDisplay(bool _Instant)
        {
            OnDisplayShow.Invoke(_Instant);
        }

        public virtual void HideDisplay(bool _Instant)
        {
            OnDisplayHide.Invoke(_Instant);
        }

        //Implementing the INotificationDisplayable interface
        public virtual void HidingFinished()
        {

        }
#endregion

#region NOTIFICATIONS

        /// <summary>
        /// Call this to show new notification item with supplied data
        /// </summary>
        /// <param name="_NotificationStatus">Data to show in notification</param>
        /// <returns></returns>
        public virtual NotificationStatus ShowNotification(NotificationData _NotificationData)
        {
            if (InitComplete == false)
                Init();

            if (CanShowNotification() == false)
                return new NotificationStatus(NotificationStatusEnum.Skipped, null, null);

            return ShowNotification(GetFreeNotificationStatusItem(_NotificationData));
        }

   
        protected virtual NotificationStatus ShowNotification(NotificationStatus _NotificationStatus)
        {
            // Make sure required items are setup and display is not disabled
            if (CanShowNotification() == false)
                return _NotificationStatus;

            // If display is on cooldown and we are allowed to queue launch coroutine to retry after the cooldown
            if (IsDisplayOnCooldown())
            {
                if (QueuingEnabled == false && QueuedNotifications.Count + ActiveNotificationItems.Count >= MaxNotifications)
                    return _NotificationStatus;

                _NotificationStatus.SetStatus(NotificationStatusEnum.Queued, false);
                QueuedNotifications.Add(_NotificationStatus);

                if (RetriggerCoroutine != null)
                    StopCoroutine(RetriggerCoroutine);


                RetriggerCoroutine = NotificationRetriggerCheckDelayed(Time.time - LastNotificationTime);
                StartCoroutine(RetriggerCoroutine);
                if (QueuingEnabled == false)
                    LastNotificationTime = -1;

                return _NotificationStatus;
            }
            
            // If display is full or on pause, queue up notification 
            if (IsNotificationQueueFull() || IsPaused)
            {
                if (QueuingEnabled == false)
                    return _NotificationStatus;

                _NotificationStatus.SetStatus(NotificationStatusEnum.Queued, false);
                QueuedNotifications.Add(_NotificationStatus);
                NotificationQueueCheck();
                return _NotificationStatus;
            }
            //If we are all good show notification Item
            else
            {
                return ShowNotificationItem(_NotificationStatus);
            }
        }
        bool IsAddressable(int _StyleVariationID)
        {
#if UNS_USE_ADDRESSABLES
            return AddressableNotificationItemPrefabs.Count > _StyleVariationID;
#endif
            return false;
        }
        protected virtual NotificationStatus ShowNotificationItem(NotificationStatus _NotificationStatus)
        {

            //Process data through assigned data processors 
            _NotificationStatus.NotificationData = ProcessData(_NotificationStatus.NotificationData);

            //We are using addressable and item is not loaded
#if UNS_USE_ADDRESSABLES
            if (_NotificationStatus.NotificationItem == null && IsAddressable(_NotificationStatus.NotificationData.StyleVariationID) && AddressableNotificationItemPrefabs[_NotificationStatus.NotificationData.StyleVariationID].LoadedItem == null)
            {
                AddressableNotificationItemPrefabs[_NotificationStatus.NotificationData.StyleVariationID].OnLoadingComplete.AddListener(AddressableLoadingFinished);
                StartCoroutine(AddressableNotificationItemPrefabs[_NotificationStatus.NotificationData.StyleVariationID].LoadPrefabAndWait(this, _NotificationStatus));
                _NotificationStatus.SetStatus(NotificationStatusEnum.Queued, true);
                return _NotificationStatus;
            }
#endif

            if (_NotificationStatus.NotificationItem == null)
                _NotificationStatus.NotificationItem = SpawnNotificationItem(GetVariationID(_NotificationStatus.NotificationData.StyleVariationID));

     
            return FinishShowingNotification(_NotificationStatus);
        }

        NotificationStatus FinishShowingNotification(NotificationStatus _NotificationStatus)
        {
            NotificationItem _Item = _NotificationStatus.NotificationItem;

            _NotificationStatus.NotificationItem = _Item;
            ActiveNotificationItems.Add(_NotificationStatus); //add item to active notification so we can track active items 
            if (_Item.gameObject.activeSelf == false) //Fixed the autoadvance
                _Item.gameObject.SetActive(true);
            _Item.Show(_NotificationStatus); // Feed new data into notification 
            _NotificationStatus.SetStatus(NotificationStatusEnum.Shown, false);

            //handle spawn direction
            if (SpawnDirection == NotificationSpawnDirection.Top)
                _Item.transform.SetAsFirstSibling();
            else
                _Item.transform.SetAsLastSibling();

            LastNotificationTime = Time.time; //Save time notification item was show to track display cooldown   

            OnItemShown.Invoke(_Item);
            return _NotificationStatus;
        }

#if UNS_USE_ADDRESSABLES
        protected virtual void AddressableLoadingFinished(NotificationStatus _NotificationStatus)
        {
            AddressableNotificationItemPrefabs[_NotificationStatus.NotificationData.StyleVariationID].OnLoadingComplete.RemoveListener(AddressableLoadingFinished);
            if (AddressableNotificationItemPrefabs[_NotificationStatus.NotificationData.StyleVariationID].LoadedItem != null)
                _NotificationStatus.NotificationItem = SpawnNotificationItem(GetVariationID(_NotificationStatus.NotificationData.StyleVariationID));
            FinishShowingNotification(_NotificationStatus);
        }
#endif

        /// <summary>
        /// Action hooken into Notification item OnItemHidingFinished to manage active items and queue 
        /// </summary>
        /// <param name="_Item">Notification Item that finished its hiding routine</param>
        protected virtual void ItemHidingFinished(NotificationItem _Item)
        {
            if (ActiveNotificationItems.Contains(_Item.Status))
            {
                ActiveNotificationItems.Remove(_Item.Status);

                if (UsePooling)
                {
                    PooledNotificationItems[GetVariationID(_Item.StyleVariationID)].Variation.Add(_Item.Status);
                    if (DisableGameObjectOnHiddenItems)
                        _Item.gameObject.SetActive(false);
                }
                else
                {
                    Destroy(_Item.gameObject);
#if UNS_USE_ADDRESSABLES

                    if (UnloadAddressableWhenNotificationIsHidden && IsAddressable(_Item.Status.NotificationData.StyleVariationID))
                    {
                        bool _AnotherInstanceExists = false;
                        for (int i = 0; i < ActiveNotificationItems.Count; i++)
                        {
                            if (ActiveNotificationItems[i].NotificationData.StyleVariationID == _Item.Status.NotificationData.StyleVariationID)
                            {
                                _AnotherInstanceExists = true;
                                break;
                            }
                        }
                        if (_AnotherInstanceExists == false)
                            AddressableNotificationItemPrefabs[_Item.Status.NotificationData.StyleVariationID].UnloadItem();
                    }
#endif
                }

                NotificationQueueCheck(false);
            }
            _Item.Status.SetStatus(NotificationStatusEnum.Hidden, false);
            OnItemHidden.Invoke(_Item);
        }

        /// <summary>
        /// Coroutine used to advance queue after cooldown 
        /// </summary>
        /// <param name="_Delay">Time until cooldown is finished</param>
        /// <returns></returns>
        protected virtual IEnumerator NotificationRetriggerCheckDelayed(float _Delay)
        {
            yield return new WaitForSeconds(_Delay + 0.01f);
            NotificationQueueCheck();
        }

        /// <summary>
        /// Check if display is ready to show queued item 
        /// </summary>
        /// <param name="_CheckAutoAdvance"></param>
        protected virtual void NotificationQueueCheck(bool _CheckAutoAdvance = true)
        {
            if (IsDisabled || IsPaused )
                return;

            if (QueuedNotifications.Count > 0)
            {
                // if we have room to show new notification show it from queued items
                if (MaxNotifications < 0 || ActiveNotificationItems.Count < MaxNotifications)
                {
                    ShowNotification(QueuedNotifications[0]);
                    QueuedNotifications.RemoveAt(0);
                    if (QueuedNotifications.Count > 0 && ActiveNotificationItems.Count < MaxNotifications || AutoAdvanceQueue)
                    {
                        RetriggerCoroutine = NotificationRetriggerCheckDelayed(NotificationCooldownTime);
                        StartCoroutine(RetriggerCoroutine);
                    }
                }
                else
                {
                    // If AutoAdvance is 
                    if (AutoAdvanceQueue && _CheckAutoAdvance)
                    {
                        ActiveNotificationItems[0].NotificationItem.Hide(AutoAdvanceQueueInstantHide);
                    }
                }
            }
        }

        protected virtual NotificationStatus GetFreeNotificationStatusItem(NotificationData _Data)
        {
            //Get variation ID 
            int _Variation = GetVariationID(_Data.StyleVariationID);

            NotificationStatus _Item = null;
            // If we use pooling retrieve pooled item if it exists
            if (UsePooling && PooledNotificationItems[_Variation].Variation.Count > 0)
            {
                _Item = PooledNotificationItems[_Variation].Variation[0];
                _Item.NotificationData = _Data;
                _Item.SetStatus(NotificationStatusEnum.Skipped, true);
                PooledNotificationItems[_Variation].Variation.RemoveAt(0);
                //if (DisableGameObjectOnHiddenItems)
                //    _Item.NotificationItem.gameObject.SetActive(true);
            }
            // if not spawn a new item 
            else
            {
                _Item = new NotificationStatus(NotificationStatusEnum.Skipped, _Data, null);
            }
            return _Item;
        }

        protected virtual NotificationItem SpawnNotificationItem(int _Variation)
        {

            NotificationItem _Item = null;
#if UNS_USE_ADDRESSABLES
            if (IsAddressable(_Variation))
                _Item = Instantiate(AddressableNotificationItemPrefabs[_Variation].LoadedItem, NotificationItemSpawnParent) as NotificationItem;
            else
                _Item =  Instantiate(NotificationItemPrefabs[_Variation], NotificationItemSpawnParent) as NotificationItem;

#else
            _Item =  Instantiate(NotificationItemPrefabs[_Variation], NotificationItemSpawnParent) as NotificationItem;
#endif
            _Item.OnHidingFinished.AddListener(ItemHidingFinished); //listen to hiding finished event so we know when queue is cleared up

            return _Item;
        }

#endregion

#region BULKOPERATIONS
        /// <summary>
        /// Disable receiving of any new notifications. To Hide as well call HideAll.
        /// </summary>
        public virtual void DisableDisplay()
        {
            IsDisabled = true;
        }

        /// <summary>
        /// Enable receiving of any new notifications. To Hide as well call HideAll.
        /// </summary>
        public virtual void EnableDisplay()
        {
            IsDisabled = false;
        }

        /// <summary>
        /// Pause showing any new notifications and queue them.
        /// </summary>
        public virtual void PauseDisplay()
        {
            IsPaused = true;
        }

        /// <summary>
        /// Unpause showing new notifications. 
        /// </summary>
        public virtual void UnpauseDisplay()
        {
            IsPaused = false;
            NotificationQueueCheck();
        }

        /// <summary>
        /// Hide All Spawned Notification Items 
        /// </summary>
        /// <param name="_Instant">Whether to call with Instant Option</param>
        public virtual void HideAll(bool _Instant = false)
        {
            for (int i = 0; i < ActiveNotificationItems.Count; i++)
            {
                ActiveNotificationItems[i].NotificationItem.Hide(_Instant);
            }
        }

        /// <summary>
        /// Show All Spawned Notification Items 
        /// </summary>
        /// <param name="_Instant">Whether to call with Instant Option</param>
        public virtual void UnhideAll(bool _Instant = false)
        {
            for (int i = 0; i < ActiveNotificationItems.Count; i++)
            {
                ActiveNotificationItems[i].NotificationItem.Show(_Instant);
            }
        }

        /// <summary>
        /// This will clear all queued notifications.
        /// </summary>
        /// <param name="_VariationID">If this is not -1, than only queued notificaiton of this index will be cleared</param>
        public virtual void ClearQueues(int _VariationID = -1)
        {
            if (_VariationID == -1)
            {
                QueuedNotifications.Clear();
            }
            else
            {
                List<NotificationStatus> _FilteredNotifications = new List<NotificationStatus>();
                for (int i = 0; i < QueuedNotifications.Count; i++)
                {
                    if (QueuedNotifications[i].NotificationData.StyleVariationID != _VariationID)
                        _FilteredNotifications.Add(QueuedNotifications[i]);
                }
                QueuedNotifications.Clear();
                QueuedNotifications.AddRange(_FilteredNotifications);
                _FilteredNotifications = null;
            }
        }
#endregion

#region HELPERS

        /// <summary>
        /// Process Data through any custom data processors assigned to Notification Manager
        /// </summary>
        /// <param name="_NotificationData">Data to process</param>
        /// <returns></returns>
        protected virtual NotificationData ProcessData(NotificationData _NotificationData)
        {
            for (int i = 0; i < DataProcessors.Count; i++)
            {
                if (DataProcessors[i] == null)
                {
                    Debug.LogError("Ultimate Notification System: Notification Manager - DataProcessor is null at index:" + i.ToString());
                }
                else
                {
                    _NotificationData = DataProcessors[i].GetProcessedData(_NotificationData);
                }
            }
            return _NotificationData;
        }

        protected virtual bool IsDisplayOnCooldown()
        {
            return (LastNotificationTime != -1 && ((Time.time - LastNotificationTime) < NotificationCooldownTime));
        }

        protected virtual bool CanShowNotification()
        {
            if (IsDisabled)
            {
                Debug.Log("Ultimate Notification System: Notification Display - " + gameObject.name + " Display is disabled, notification will be ignored.");
                return false;
            }

#if UNS_USE_ADDRESSABLES
            if (AddressableNotificationItemPrefabs.Count == 0 && NotificationItemPrefabs.Count == 0)
            {
                Debug.LogError("Ultimate Notification System: Notification Display - " + gameObject.name + "Has no Notification Item Prefabs assigned.");
                return false;
            }
#else
            if (NotificationItemPrefabs.Count == 0)
            {
                Debug.LogError("Ultimate Notification System: Notification Display - " + gameObject.name + "Has no Notification Item Prefabs assigned.");
                return false;
            }
#endif
            return true;
        }

        protected virtual bool IsNotificationQueueFull()
        {
            return MaxNotifications < 0 ? false : ActiveNotificationItems.Count >= MaxNotifications;
        }

        protected virtual int GetVariationID(int _StyleVariationID)
        {

#if UNS_USE_ADDRESSABLES
            if (IsAddressable(_StyleVariationID) == false && NotificationItemPrefabs.Count <= _StyleVariationID)
            {
                    Debug.LogError("Ultimate Notification System: Notification Display - " + gameObject.name +
                        " Style Variation ID in Notification Data is higher than number of NotificationItem prefabs ssigned to this Notification Display. To avoid error system is defaulting to first NotificationItem Prefab on the list."
                        + "To Fix this error either lower StyleVariationID in notification data you sent into this Display, or add new NotificationItem Prefabs to NotificationItemPrefabs list on this display."
                        + "Please note that notification count starts with 0 index. StyleVariationID of 0 = first prefab assigned in NotificationItemPrefabs list, StyleVariationID of 1 = second item etc.");
                    return 0;
            }
#else

            if (NotificationItemPrefabs.Count <= _StyleVariationID)
            {
                Debug.LogError("Ultimate Notification System: Notification Display - " + gameObject.name +
                    " Style Variation ID in Notification Data is higher than number of NotificationItem prefabs ssigned to this Notification Display. To avoid error system is defaulting to first NotificationItem Prefab on the list."
                    + "To Fix this error either lower StyleVariationID in notification data you sent into this Display, or add new NotificationItem Prefabs to NotificationItemPrefabs list on this display."
                    + "Please note that notification count starts with 0 index. StyleVariationID of 0 = first prefab assigned in NotificationItemPrefabs list, StyleVariationID of 1 = second item etc.");
                return 0;
            }
#endif
            else return _StyleVariationID;
        }
#if UNS_USE_ADDRESSABLES
        protected virtual void UnloadAddressables()
        {
            foreach (var _AddressablePrefab in AddressableNotificationItemPrefabs)
            {
                _AddressablePrefab.UnloadItem();
            }
        }
#endif
#endregion

    }
}