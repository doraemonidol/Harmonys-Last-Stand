using System;
using System.Collections.Generic;
using Common;
using MockUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation
{
    public abstract class PWeapon : PresentationObject
    {
        [SerializeField] protected List<PSkill> normalSkills;
        [SerializeField] protected List<PSkill> specialSkills;
        protected Identity Owner;
        
        public void SetOwner(Identity owner)
        {
            Owner = owner;
            
            for (int i = 0; i < normalSkills.Count; i++)
            {
                normalSkills[i].Owner = owner;
            }
            
            for (int i = 0; i < specialSkills.Count; i++)
            {
                specialSkills[i].Owner = owner;
            }
        }
        
        public List<PSkill> GetNormalSkills()
        {
            return normalSkills;
        }
        
        public List<PSkill> GetSpecialSkills()
        {
            return specialSkills;
        }
        
        public List<PSkill> GetSkills()
        {
            var skills = new List<PSkill>();
            skills.AddRange(normalSkills);
            skills.AddRange(specialSkills);
            return skills;
        }
    }
}