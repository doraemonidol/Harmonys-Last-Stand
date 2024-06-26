using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class RiffRumble : AcSkill
    {
        public RiffRumble(IWeapon owner) : base(owner)
        {
        }

        public RiffRumble(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.Stunt);
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 30
            };
            target.ReceiveEffect(EffectHandle.GetHit);
        }
    }
}