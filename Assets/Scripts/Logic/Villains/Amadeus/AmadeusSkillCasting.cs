using System;
using System.Collections.Generic;
using DTO;
using Logic.Villains.States;

namespace Logic.Villains.Amadeus
{
    public class AmadeusSkillCasting : SkillCasting
    {
        public AmadeusSkillCasting(Villain owner, Dictionary<string, object> data = null) : base(owner, data)
        {
        }
        
        public AmadeusSkillCasting(Villain owner, SkillCastingSession session, Dictionary<string, object> data = null) : base(owner, session, data)
        {
        }

        protected override int OnStateEnter(Dictionary<string, object> data = null)
        {
            var random = new Random();
            var randomSkillId = random.Next(0, SkillIds.Count);
            return SkillIds[randomSkillId];
        }

        protected override void OnStateExit(Dictionary<string, object> data = null)
        {
            throw new NotImplementedException();
        }

        public override void OnStateUpdate(Dictionary<string, object> data = null)
        {
            throw new NotImplementedException();
        }
    }
}