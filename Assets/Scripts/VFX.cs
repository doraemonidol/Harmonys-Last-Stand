using System;
using UnityEngine;

[Serializable]
public class VFX
{
    public GameObject vfx;
    public float duration;
    public float affectCooldown = 0;

    public Transform transform;
    public bool autoDestroy;
    
    public bool HasVFX()
    {
        return vfx;
    }
    
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}