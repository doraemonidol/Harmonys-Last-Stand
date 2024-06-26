using System;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons.Attributes;

namespace Logic.Weapons
{
    public interface IWeapon
    {
        public static IWeapon TransformInto(int name)
        {
            return name switch
            {
                WeaponHandle.Violin => new Violin(),
                WeaponHandle.Saxophone => new Saxophone(),
                WeaponHandle.Flute => new Flute(),
                WeaponHandle.Piano => new Piano(),
                WeaponHandle.SuperBass => new SuperBass(),
                WeaponHandle.Guitar => new Guitar(),
                _ => throw new Exception("Weapon not found.")
            };
        }
        
        public void Trigger(int index);
    }
}
