using System;
using System.Collections.Generic;

namespace Logic.Troops.DeathStrategy
{
    public class DoubleUpStrategy : IDeathStrategy
    {
        public void Execute(Dictionary<string, object> args)
        {
            try
            {
                var thisTroop = (Troop)args["troop"];
                var visitor = new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "death",
                        ["next"] = "double_up",
                    }
                };
                thisTroop.NotifySubscribers(visitor);
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