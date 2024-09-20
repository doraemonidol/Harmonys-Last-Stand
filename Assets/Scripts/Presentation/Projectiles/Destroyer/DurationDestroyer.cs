using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationDestroyer : MonoBehaviour
{
    [SerializeField] private float duration = 10f;
    private float _startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _startTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
