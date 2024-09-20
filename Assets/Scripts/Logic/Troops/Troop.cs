using System;
using Logic.Helper;
using Logic.Troops.DeathStrategy;
using Logic.Villains;
using Logic.Weapons;
using static Common.GameStats;

namespace Logic.Troops
{
    public class Troop : Villain
    {
        public int Handle { get; set; }
        
        private IDeathStrategy _deathStrategy;
        
        public Troop() : base(
            TROOP_HEALTH,
            TROOP_ATKSPEED,
            TROOP_MOVSPEED,
            TROOP_ATTACK
        )
        {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.TrWeapon);
        }
        
        public Troop(Troop another) : base(another)
        {
        }

        protected Troop(IDeathStrategy deathStrategy) : base()
        {
            _deathStrategy = deathStrategy;
        }

        public Troop(Troop another, IDeathStrategy strategy) : base(another)
        {
            _deathStrategy = strategy;
        }
        
        public static Troop TransformInto(int troopType)
        {
            return troopType switch
            {
                TroopHandle.DUGEON_TROOP1 => null,
                _ => throw new ArgumentOutOfRangeException(nameof(troopType), troopType, null)
            };
        }
    }
}