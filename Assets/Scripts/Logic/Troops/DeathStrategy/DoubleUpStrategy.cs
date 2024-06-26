using System;
using System.Collections.Generic;
using Logic.Facade;

namespace Logic.Troops.DeathStrategy
{
    public class DoubleUpStrategy : IDeathStrategy
    {
        public void Execute(Dictionary<string, object> args)
        {
            try
            {
                var world = LogicWorld.GetInstance();
                // var presTroop1 = new PresentationTroop();
                // var presTroop2 = new PresentationTroop();
                
                throw new Exception("Not done yet.");
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}