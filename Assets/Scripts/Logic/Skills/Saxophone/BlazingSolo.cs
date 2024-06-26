using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Saxophone
{
    public class BlazingSolo : AcSkill
    {
        public BlazingSolo(IWeapon owner) : base(owner)
        {
        }

        public BlazingSolo(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 10
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            var args1 = new EventDto
            {
                [EffectHandle.HpDrain] = 3,
                ["timeout"] = 10,
            };
            target.ReceiveEffect(EffectHandle.Bleeding, args1);
        }
    }
}