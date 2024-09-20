using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class RiffRumble : AcSkill
    {
        public RiffRumble(Weapon owner) : base(owner)
        {
        }

        public RiffRumble(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 35 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}