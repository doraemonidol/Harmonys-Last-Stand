using System;
using System.Collections.Generic;

namespace Presentation
{
    public abstract class PWeapon : PresentationObject
    {

        protected List<PSkill> Skills;
        
        public List<PSkill> GetSkills()
        {
            return Skills;
        }


    }
}