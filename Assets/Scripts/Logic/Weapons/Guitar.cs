using Logic.Helper;
using Logic.Skills;

namespace Logic.Weapons
{
    public class Guitar : Weapon
    {
        
        public Guitar()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Guitar, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Guitar, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Guitar, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Guitar, this, 4));
        }
    }
}