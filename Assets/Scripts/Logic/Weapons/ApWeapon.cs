using Logic.Helper;
using Logic.Skills;
using Logic.Villains;

namespace Logic.Weapons
{
    public class ApWeapon : Weapon
    {
        public ApWeapon()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.ApWeapon, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.ApWeapon, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.ApWeapon, this, 3));
        }
    }
}