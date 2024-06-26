using System;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Flute
{
    public class TempestTones : AcSkill
    {
        protected TempestTones(IWeapon owner) : base(owner)
        {
        }

        protected TempestTones(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
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