using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using Logic.Helper;
using Logic.Villains.States;

namespace Logic.Villains.Maestro
{
    public class MmSkillCasting : SkillCasting
    {
        public MmSkillCasting(Villain owner, Dictionary<string, object> data = null) : base(owner, data)
        {
        }
        
        public MmSkillCasting(Villain owner, SkillCastingSession session, Dictionary<string, object> data = null) : base(owner, session, data)
        {
        }

        protected override int OnStateEnter(Dictionary<string, object> data = null)
        {
            var lastSkillId = (Session.SkillCastingResults.Count > 0) ? Session.SkillCastingIds[^1] : -1;
            var castingResult = (Session.SkillCastingResults.Count > 0) && Session.SkillCastingResults[^1];
            if (castingResult)
            {
                var isSkill1Available = SkillIds.Contains(1);
                var isSkill2Available = SkillIds.Contains(2);
                var isSkill3Available = SkillIds.Contains(3);
                return lastSkillId switch
                {
                    1 => (isSkill3Available ? 3 : isSkill2Available ? 2: 1),
                    2 => (isSkill1Available ? 1 : isSkill3Available ? 3: 2),
                    3 => (isSkill1Available ? 1 : isSkill2Available ? 2: 3),
                    4 => (isSkill2Available ? 2 : isSkill3Available ? 3: 1),
                    _ => throw new Exception("Invalid skill id"),
                };
            }
            else
            {
                // random one skill in SkillList without using Unity Engine
                var random = new Random();
                var randomSkillId = random.Next(0, SkillIds.Count - 1);
                return SkillIds[randomSkillId];
            }
        }

        protected override void OnStateExit(Dictionary<string, object> data = null)
        {
            //throw new NotImplementedException();
        }

        public override void OnStateUpdate(Dictionary<string, object> data = null)
        {
            var isCastingDone = (bool) data?["cast"]!;
            if (!isCastingDone) return;
            OnStateExit();
            Session.SkillCastingIds.Add(ChosenSkillId);
            Session.SkillCastingTimes.Add(CustomTime.WhatIsIt());
            Owner.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "cast",
                },
                ["data"] =
                {
                    ["skill"] = 1,
                }
            });
            Owner.SetState(new ResultWaiting(Owner, Session));
        }
    }
}