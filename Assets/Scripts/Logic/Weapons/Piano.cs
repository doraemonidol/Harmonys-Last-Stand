using Logic.Helper;
using Logic.Skills;

namespace Logic.Weapons
{
    public class Piano : Weapon
    {
        public Piano()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Piano, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Piano, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Piano, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Piano, this, 4));
        }
    }
}