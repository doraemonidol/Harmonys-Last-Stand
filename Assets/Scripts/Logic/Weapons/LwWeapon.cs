using Logic.Helper;
using Logic.Skills;
using Logic.Villains;

namespace Logic.Weapons
{
    public class LwWeapon : Weapon
    {
        public LwWeapon()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.LwWeapon, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.LwWeapon, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.LwWeapon, this, 3));
        }
    }
}