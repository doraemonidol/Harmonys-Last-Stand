using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackBoxController : MonoBehaviour
{
    public float lifeTime = 20f;
    private float spawnTime;
    private bool collided = false;
    [SerializeField] private GameObject jackBox;
    [SerializeField] private VFX appearVFX;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 6f + spawnTime && !collided)
        {
            jackBox.SetActive(false);
        } 
        
        if (Time.time > spawnTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            Debug.Log("Player hit jackbox");
            StartCoroutine(StartAppearVFX());
        }
    }
    
    IEnumerator StartAppearVFX()
    {
        // GameObject vfx = Instantiate(appearVFX.vfx, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        jackBox.SetActive(true);
        // Destroy(vfx, appearVFX.duration);
        Destroy(gameObject, 1f);
    }
}
