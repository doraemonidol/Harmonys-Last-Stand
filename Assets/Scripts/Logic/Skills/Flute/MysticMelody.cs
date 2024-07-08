using System;
using System.Collections.Generic;
using System.Threading;
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

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["timeout"] = 10,
            };
            attacker.ReceiveEffect(EffectHandle.Shielded, args);
        }
    }
}