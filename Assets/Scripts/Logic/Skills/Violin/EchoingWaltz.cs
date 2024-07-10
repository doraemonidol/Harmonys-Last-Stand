using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class EchoingWaltz : AcSkill
    {
        public EchoingWaltz(Weapon owner) : base(owner)
        {
        }

        public EchoingWaltz(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 25 * (100 + boostAmount) / 100;
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
            });
        }
    }
}