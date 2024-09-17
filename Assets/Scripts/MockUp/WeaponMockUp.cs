using System.Collections.Generic;
using Common;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using UnityEngine;

namespace MockUp
{
    public class WeaponMockUp : PWeapon
    {
        public override void OnEnable()
        {
            LogicLayer.GetInstance().Instantiate(EntityType.GetEntityType(entityType), this);
            // Debug.Log("Weapon: " + entityType.ToString() + " ("+ this.LogicHandle + ") with " + normalSkills.Count + " normal skills and " + specialSkills.Count + " special skills");
            // foreach (var skill in normalSkills)
            // {
            //     Debug.Log("Normal skill: " + skill.LogicHandle);
            // }
            //
            // foreach (var skill in specialSkills)
            // {
            //     Debug.Log("Special skill: " + skill.LogicHandle);
            // }
        }

        public override void Update()
        {
            
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            
        }
    }
}