using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animmal.NotificationSystem
{
    [System.Serializable]
    public class NotificationAudioPlayerItem
    {
        public AudioSource AudioSource;
        public Image AudioPlayerProgressBar;

        IEnumerator ProgressBarCoroutine;
        MonoBehaviour MonoBehaviourRef;

        private WaitForEndOfFrame _WaitForEndOfFrame;
        public WaitForEndOfFrame WaitForEndOfFrame { get { if (_WaitForEndOfFrame == null) _WaitForEndOfFrame = new WaitForEndOfFrame(); return _WaitForEndOfFrame; } }


        public bool ErrorCheck(string _GameObjectName)
        {
            if (AudioSource == null)
            {
                Debug.LogError("Ultimate Notification System - ItemDisplayDataAudioPlayer - Video Player Missing on gameobject: " + _GameObjectName);
                return false;
            }
            if (AudioPlayerProgressBar == null)
            {
                Debug.LogWarning("Ultimate Notification System - ItemDisplayDataAudioPlayer - AudioPlayerProgressBar Missing on gameobject: " + _GameObjectName + " Won't display play progress");
            }

            return true;
        }

        IEnumerator AnimateProgressBar()
        {
            while (true)
            {
                AudioPlayerProgressBar.fillAmount = AudioSource.time/AudioSource.clip.length;
                yield return WaitForEndOfFrame;
            }
        }

        public void Load(AudioClip _Clip, MonoBehaviour _Monobehavior)
        {
            MonoBehaviourRef = _Monobehavior;
            AudioSource.clip = _Clip;
            AudioSource.Play();
            if (AudioPlayerProgressBar != null)
            {
                ProgressBarCoroutine = AnimateProgressBar();
                MonoBehaviourRef.StartCoroutine(ProgressBarCoroutine);
            }
        }

        public void Unload()
        {
            AudioSource.Stop();
            AudioSource.clip = null;
            if (MonoBehaviourRef != null && ProgressBarCoroutine!= null)
            {
                MonoBehaviourRef.StopCoroutine(ProgressBarCoroutine);
            }
        }
    }
    [AddComponentMenu("Animmal/DisplayBehavior/Display Audio Player")]
    public class ItemDisplayDataAudioPlayer : ItemDisplayDataBase
    {
        public List<NotificationAudioPlayerItem> AudioPlayerItems = new List<NotificationAudioPlayerItem>();

        protected override void Init()
        {
            base.Init();
            NotificationItem.OnHidingFinished.AddListener(Unload);
        }

        private void Unload(NotificationItem _Item)
        {
            for (int i = 0; i < AudioPlayerItems.Count; i++)
            {
                if (AudioPlayerItems[i].ErrorCheck(gameObject.name))
                    AudioPlayerItems[i].Unload();
            }
        }

        protected override void DataAssigned(NotificationData _Data)
        {
            for (int i = 0; i < _Data.AudioClips.Count; i++)
            {
                if (i < AudioPlayerItems.Count)
                {
                    if (AudioPlayerItems[i].ErrorCheck(gameObject.name))
                        AudioPlayerItems[i].Load(_Data.AudioClips[i], this);
                }
            }

            base.DataAssigned(_Data);
        }
    }
}
