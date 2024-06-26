using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class ResonantStrike : AcSkill
    {
        public ResonantStrike(IWeapon owner) : base(owner)
        {
        }

        public ResonantStrike(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 20
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            var args1 = new EventDto
            {
                ["timeout"] = 500,
            };
            target.ReceiveEffect(EffectHandle.Stunt, args1);
        }
    }
}