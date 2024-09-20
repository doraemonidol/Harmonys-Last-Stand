using System.Collections.Generic;

namespace Logic.Troops.DeathStrategy
{
    public interface IDeathStrategy
    {
        public void Execute(Dictionary<string, object> args);

        public void GetStrategyType();
    }
}