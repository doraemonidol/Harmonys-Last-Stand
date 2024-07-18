using Logic.Helper;
using Logic.Skills;

namespace Logic.Weapons
{
    public class SuperBass : Weapon
    {
        public SuperBass()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.SuperBass, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.SuperBass, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.SuperBass, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.SuperBass, this, 4));
        }
    }
}