using System;
using System.Collections.Generic;
using System.Threading;
using Common.Context;
using DTO;
using Logic;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Flute
{
    public class MysticMelody : AcSkill
    {
        
        public MysticMelody(Weapon owner) : base(owner)
        {
            
        }

        public MysticMelody(Weapon owner, long coolDown) : base(owner: owner, coolDownTime: coolDown)
        {
        }

        public override void Activate(ICharacter activator)
        {
            // var boostAmount = GameContext.GetInstance().Get("dmg+");
            // var finalDmg = 10 * (100 + boostAmount) / 100;
            base.Activate(activator);
            var args = new EventDto
            {
                ["timeout"] = 10,
            };
            User.ReceiveEffect(EffectHandle.Resistance, args);
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}