using System;
using System.Collections.Generic;
using MockUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation
{
    public abstract class PWeapon : PresentationObject
    {
        [SerializeField] protected List<PSkill> normalSkills;
        [SerializeField] protected List<PSkill> specialSkills;
        
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