using System;
using System.Collections.Generic;
using Logic.Helper;
using Logic.Skills;
using Logic.Villains;

namespace Logic.Weapons
{
    public abstract class Weapon : LogicObject
    {
        public List<AcSkill> Skills { get; } = new();

        public static Weapon TransformInto(int name)
        {
            return name switch
            {
                WeaponHandle.Violin => new Violin(),
                WeaponHandle.Saxophone => new Saxophone(),
                WeaponHandle.Flute => new Flute(),
                WeaponHandle.Piano => new Piano(),
                WeaponHandle.SuperBass => new SuperBass(),
                WeaponHandle.Guitar => new Guitar(),
                WeaponHandle.ApWeapon => new ApWeapon(),
                WeaponHandle.LwWeapon => new LwWeapon(),
                WeaponHandle.MmWeapon => new MmWeapon(),
                WeaponHandle.TrWeapon => new TroopWeapon(),
                _ => throw new Exception("Weapon not found.")
            };
        }

        public void Trigger(int index, ICharacter activator)
        {
            var skill = Get(index);
            skill.Activate(activator);
        }
        
        private bool IfSkillExists(AcSkill skill)
        {
            return Skills.Contains(skill);
        }

        public void Add(AcSkill skill)
        {
            Skills.Add(skill);
        }
        
        private AcSkill Get(int index)
        {
            if (index > Skills.Count || index < 0)
            {
                throw new System.Exception(
                    "At WAttributes: The skill index is out of range. " +
                    $"Size: {Skills.Count}. Query: {index}."
                );
            }
            return Skills[index];
        }
    }
}
