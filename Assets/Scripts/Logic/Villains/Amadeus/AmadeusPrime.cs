using Common;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Villains.Amadeus
{
    public class AmadeusPrime : Villain
    {
        public AmadeusPrime() 
        : base(
            GameStats.AMADEUS_HEALTH, 
            GameStats.AMADEUS_ATKSPEED, 
            GameStats.AMADEUS_MOVSPEED,
            GameStats.AMADEUS_ATTACK
        ) {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.ApWeapon);
            State = new AmadeusSkillCasting(this);
        }
        
        public AmadeusPrime(LogicObject another) : base(another)
        {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.ApWeapon);
            State = new AmadeusSkillCasting(this);
        }
    }
}