using System.Collections.Generic;
using System.Linq;
using Animmal.NotificationSystem;
using DTT.Utils.Extensions;
using Presentation.GUI;
using Presentation.Maestro;
using Runtime;
using TMPro;
using UnityEngine;

namespace Presentation.Manager
{
    public class GraveManager : MonoBehaviorInstance<GraveManager>
    {
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private GameObject gravePrefab;
        [SerializeField] private int graveCount = 3;
        [SerializeField] private TMP_Text progressText;
        private List<bool> isFound = new List<bool>();
        [SerializeField] private NotificationHelperComponent notificationHelperComponent;
    
        // Start is called before the first frame update
        void Start()
        {
            notificationHelperComponent = this.GetComponent<NotificationHelperComponent>();
            GetWaypoints();
            waypoints.Shuffle();
        
            for (int i = 0; i < Mathf.Min(waypoints.Count, graveCount); i++)
            {
                var grave = Instantiate(gravePrefab, waypoints[i].position, waypoints[i].rotation);
                grave.transform.SetParent(transform);
                grave.AddComponent<GraveInfo>().Initialize(i);
            }
            
            for (int i = 0; i < waypoints.Count; i++)
            {
                isFound.Add(false);
            }progressText.text = "0/" + graveCount;
        }

        // Update is called once per frame
        void Update()
        {
            // Check key combination of number 1 2 3 4 5 6
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetFound(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetFound(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetFound(2);
            }
        }

        private void GetWaypoints()
        {
            var wps = GameObject.FindGameObjectsWithTag("Grave");
            foreach (var wp in wps)
            {
                waypoints.Add(wp.transform);
            }
        }
        
        public void SetFound(int index)
        {
            isFound[index] = true;
            progressText.text = isFound.Count(x => x) + "/" + graveCount;
            if (isFound.Count(x => x) == graveCount)
            {
                SceneManager.Instance.UnlockSceneTrigger();
                notificationHelperComponent.ShowNotification();
            }
        }
    }
}
