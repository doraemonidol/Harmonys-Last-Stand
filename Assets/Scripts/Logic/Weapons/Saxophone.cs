using Logic.Helper;
using Logic.Skills;

namespace Logic.Weapons
{
    public class Saxophone : Weapon
    {
        
        public Saxophone()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Saxophone, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Saxophone, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Saxophone, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Saxophone, this, 4));
        }
    }
}