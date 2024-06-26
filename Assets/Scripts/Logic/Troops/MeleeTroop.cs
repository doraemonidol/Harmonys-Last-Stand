using Logic.Troops.DeathStrategy;

namespace Logic.Troops
{
    public class MeleeTroop : Troop
    {
        public MeleeTroop(IDeathStrategy deathStrategy) : base(deathStrategy)
        {
        }
    }
}