using System;
using Logic.Helper;
using Logic.Troops.DeathStrategy;

namespace Logic.Troops
{
    public class Troop : LogicObject
    {
        public int Handle { get; set; }
        
        private IDeathStrategy _deathStrategy;
        
        public Troop(Troop another) : base(another)
        {
        }
        
        public Troop(IDeathStrategy deathStrategy) : base()
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