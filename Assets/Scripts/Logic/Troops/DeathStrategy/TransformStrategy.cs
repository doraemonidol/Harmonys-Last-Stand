using System;
using System.Collections.Generic;

namespace Logic.Troops.DeathStrategy
{
    public class TransformStrategy : IDeathStrategy
    {
        public void Execute(Dictionary<string, object> args)
        {
            try
            {
                var thisTroop = (Troop)args["troop"];
                var transType = (int)args["next_type"];
                var newTroopArgs = new Dictionary<string, object>();
                var transformStrategy = new TransformStrategy();
                var newTroop = new Troop(thisTroop, transformStrategy);
                thisTroop.Disconnect();
                var visitor = new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "",
                    }
                };
                newTroop.NotifySubscribers(visitor);
                throw new Exception("Not done yet.");
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void GetStrategyType()
        {
            throw new NotImplementedException();
        }
    }
}