using System;
using System.Collections;
using Common;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons.Attributes;

namespace Logic.Weapons
{
    public class Violin : Weapon
    {
        private WAttributes _attributes;
        
        public Violin()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Violin, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Violin, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Violin, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Violin, this, 4));
        }
    }
}