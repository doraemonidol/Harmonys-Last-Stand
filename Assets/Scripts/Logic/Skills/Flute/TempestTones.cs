using System;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;
using UnityEngine;

namespace Logic.Skills.Flute
{
    public class TempestTones : AcSkill
    {
        public TempestTones(Weapon owner) : base(owner)
        {
        }

        public TempestTones(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 5 * (100 + boostAmount) / 100;
            Debug.Log("TempestTones: " + finalDmg);
            var args1 = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
                ["timeout"] = 6,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args1);
            target.ReceiveEffect(EffectHandle.Sleepy, args1);
        }
    }
}