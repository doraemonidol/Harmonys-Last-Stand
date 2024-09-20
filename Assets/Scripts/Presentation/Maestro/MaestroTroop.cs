using System.Collections.Generic;
using System.Linq;
using DTT.Utils.Extensions;
using Presentation.GUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Presentation.Maestro
{
    public class MaestroTroop : MonoBehaviour
    {
        private Camera cam;
        public NavMeshAgent agent;
        public GameObject target;
        public int WaypointID;
        private List<GameObject> waypoints;
        public bool CanSeeTarget = false;
        public bool Blind = false;
        public GameObject activeWaypoint;
    
        // Start is called before the first frame update
        void Start()
        {
            waypoints = new List<GameObject>();
            cam = Camera.main;
        }

        private void GetWaypoints()
        {
            switch (WaypointID)
            {
                case 1:
                {
                    var wps = GameObject.FindGameObjectsWithTag("WPM1");
                    foreach (var wp in wps)
                    {
                        waypoints.Add(wp);
                    }

                    break;
                }
                case 2:
                {
                    var wps = GameObject.FindGameObjectsWithTag("WPM2");
                    foreach (var wp in wps)
                    {
                        waypoints.Add(wp);
                    }

                    break;
                }
                case 3:
                {
                    var wps = GameObject.FindGameObjectsWithTag("WPM3");
                    foreach (var wp in wps)
                    {
                        waypoints.Add(wp);
                    }

                    break;
                }
                default:
                    throw new System.Exception("Invalid Waypoint ID");
            }
        }
    
        private float DistanceTo(GameObject obj)
        {
            var distance = Vector3.Distance(this.transform.position, obj.transform.position);
            return distance;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.IsGamePaused)
                return;
            
            Blind = false;
        
            CanSeeTarget = false;
        
            if (waypoints.Count == 0)
            {
                GetWaypoints();
            }
        
            if (activeWaypoint == null)
            {
                // Please shuffle the waypoints list
                waypoints.Shuffle();
            
                activeWaypoint = waypoints[0];
                agent.SetDestination(activeWaypoint.transform.position);
            }

            // Get all the troops in the scene
            var troops = GameObject.FindGameObjectsWithTag("MaeTroop");
            var IfAllSee = troops.Any(troop => troop.GetComponent<MaestroTroop>().CanSeeTarget);

            var graves = GameObject.FindGameObjectsWithTag("Grave");
            var IsTargetNearGrave = graves.Select(grave =>
                Vector3.Distance(target.transform.position, grave.transform.position)).Any(distance => distance <= 20);
            IfAllSee |= IsTargetNearGrave;

            // Check if target is not in blind house
            var blindHouse = GameObject.FindGameObjectsWithTag("BlindHouse");
            if (blindHouse.Select(bh =>
                    Vector3
                        .Distance(target.transform.position, bh.transform.position))
                .Any(distanceFromTargetToBlindHouse => distanceFromTargetToBlindHouse <= 5))
            {
                foreach (var troop in troops)
                {
                    troop.GetComponent<MaestroTroop>().Blind = true;
                }
            }

            // Move the troop to the target
            CanSeeTarget = DistanceTo(target) <= 30;
        
            if (activeWaypoint == target && DistanceTo(target) <= 2 && !Blind)
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                target.GetComponent<PlayerMovement>().isDead = true;
                GameManager.Instance.GameOver();
                return;
            }
        
            if ((DistanceTo(target) <= 30 || IfAllSee) && !Blind)
            {
                Debug.Log("Run into the target!");
                activeWaypoint = target;
                agent.SetDestination(target.transform.position);
            }
            else if (activeWaypoint == target && ((DistanceTo(target) > 30 && !IfAllSee) || Blind))
            {
                waypoints.Shuffle();
                activeWaypoint = waypoints[0];
                agent.SetDestination(activeWaypoint.transform.position);
                return;
            }
        
            // Move the troop to the waypoint
            if (activeWaypoint == null || activeWaypoint == target || !(DistanceTo(activeWaypoint) <= 5)) return;
            var temp = waypoints[0];
            waypoints.RemoveAt(0);
            waypoints.Add(temp);
            waypoints.Shuffle();
            activeWaypoint = waypoints[0];
            agent.SetDestination(waypoints[0].transform.position);
        }
    }
}
