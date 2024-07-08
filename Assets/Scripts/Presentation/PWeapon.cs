using System;
using System.Collections.Generic;

namespace Presentation
{
    public abstract class PWeapon : PresentationObject
    {
        protected List<PSkill> NormalSkills;
        protected List<PSkill> SpecialSkills;
        
        public List<PSkill> GetNormalSkills()
        {
            return NormalSkills;
        }
        
        public List<PSkill> GetSpecialSkills()
        {
            return SpecialSkills;
        }
        
        public List<PSkill> GetSkills()
        {
            var skills = new List<PSkill>();
            skills.AddRange(NormalSkills);
            skills.AddRange(SpecialSkills);
            return skills;
        }
    }
}