using Logic.Helper;
using Logic.Skills;

namespace Logic.Weapons
{
    public class Flute : Weapon
    {
        public Flute()
        {
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Flute, this, 1));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Flute, this, 2));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Flute, this, 3));
            Skills.Add(AcSkill.TransformInto(WeaponHandle.Flute, this, 4));
        }
        
        
    }
}