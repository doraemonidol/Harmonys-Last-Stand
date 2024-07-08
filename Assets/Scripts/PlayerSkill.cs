// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using DTT.AreaOfEffectRegions;
// using UnityEngine;
// using UnityEngine.Serialization;
// using Debug = UnityEngine.Debug;
//
// public abstract class PlayerSkill : BaseSkill
// {
//     [Header("Player Skill")]
//     [SerializeField] protected RotateToMouseScript rotateToMouse;
//     
//     public void AttachRotateToMouse(RotateToMouseScript rotateToMouse)
//     {
//         this.rotateToMouse = rotateToMouse;
//     }
//     
//     public void AttachSkillContainer(GameObject skillContainer)
//     {
//         this.skillContainer = skillContainer;
//     }
//     
//     public void Start()
//     {
//         Debug.Log("Player Skill Class Start");
//         base.Start();
//     }
//     
//     // Update is called once per frame
//     void Update()
//     {
//         base.Update();
//     }
// }
