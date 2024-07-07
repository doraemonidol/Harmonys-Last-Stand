using System;
using DTO;
using Logic.Helper;
using Logic.Weapons;

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
            var args1 = new EventDto
            {
                ["timeout"] = 6,
            };
            target.ReceiveEffect(EffectHandle.Sleepy, args1);
        }
    }
}