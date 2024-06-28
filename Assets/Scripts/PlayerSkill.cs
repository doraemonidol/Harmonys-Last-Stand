using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using DTT.AreaOfEffectRegions;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public abstract class PlayerSkill : BaseSkill
{
    
    public void Start()
    {
        Debug.Log("Player Skill Class Start");
        base.Start();
    }
    
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public abstract void UpdateChannelingTime(float timeScaleFactor);
    public abstract void StartChanneling();
    public abstract void StopChanneling();
    public abstract double GetChannelingTime();
}
