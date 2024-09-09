using System.Collections;
using System.Collections.Generic;
using DTT.Utils.Extensions;
using UnityEngine;

public class GraveSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private GameObject gravePrefab;
    [SerializeField] private int graveCount = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
        waypoints.Shuffle();
        
        for (int i = 0; i < Mathf.Min(waypoints.Count, graveCount); i++)
        {
            var grave = Instantiate(gravePrefab, waypoints[i].position, waypoints[i].rotation);
            grave.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetWaypoints()
    {
        var wps = GameObject.FindGameObjectsWithTag("Grave");
        foreach (var wp in wps)
        {
            waypoints.Add(wp.transform);
        }
    }
}
