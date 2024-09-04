using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DTO;
using Logic.Facade;
using UnityEngine;

public class JackBoxController : MonoBehaviour
{
    public float lifeTime = 20f;
    private float spawnTime;
    private bool collided = false;
    public Identity MaestroLogicHandle;
    public Identity AureliaLogicHandle;
    public Identity LogicHandle;
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
        if (Time.time > 3f + spawnTime && !collided)
        {
            jackBox.SetActive(false);
        } 
        
        if (Time.time > spawnTime + lifeTime)
        {
            StartCoroutine(DestroySelf());
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
        
        // var eventd = new EventDto
        // {
        //     Event = "GET_ATTACKED",
        //     ["attacker"] = MaestroLogicHandle,
        //     ["target"] = AureliaLogicHandle,
        //     ["context"] = null,
        //     ["skill"] = LogicHandle
        // };
        // LogicLayer.GetInstance().Observe(eventd);
        
        jackBox.SetActive(true);
        // Destroy(vfx, appearVFX.duration);
        Destroy(gameObject, 1f);
    }
    
    IEnumerator DestroySelf()
    {
        jackBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
