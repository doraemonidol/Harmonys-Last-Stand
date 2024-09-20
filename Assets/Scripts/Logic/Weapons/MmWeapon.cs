using Logic.Helper;
using Logic.Skills;
using Logic.Villains;

namespace Logic.Weapons
{
    public class MmWeapon : Weapon
    {
        public MmWeapon()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.MmWeapon, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.MmWeapon, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.MmWeapon, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.MmWeapon, this, 4));
        }
    }
}