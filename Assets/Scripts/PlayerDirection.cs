using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private GameObject directionIndicator;
    private Vector3 _mousePosition;
    [SerializeField] private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        _mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y - transform.position.y));
        _mousePosition.y = 0;
        // Debug.Log(_mousePosition);
    }
    
    private void FixedUpdate()
    {
        Vector3 lookDirection = _mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.z, lookDirection.x) * Mathf.Rad2Deg - 90f;
        directionIndicator.transform.rotation = Quaternion.Euler(0, -angle, 0);
    }
}
