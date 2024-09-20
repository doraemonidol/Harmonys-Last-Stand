using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RangeDestroyer : MonoBehaviour
{
    [SerializeField] private float maximalRange = 10f;
    private Vector3 _startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) >= maximalRange)
        {
            Destroy(gameObject);
        }
    }
}
