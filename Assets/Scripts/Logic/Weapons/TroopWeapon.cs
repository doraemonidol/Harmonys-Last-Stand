using Logic.Helper;
using Logic.Skills;
using Logic.Villains;

namespace Logic.Weapons
{
    public class TroopWeapon : Weapon
    {
        public TroopWeapon()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.TrWeapon, this, 1));
        }
    }
}