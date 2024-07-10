using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class MelodicSlash : AcSkill
    {
        public MelodicSlash(Weapon owner) : base(owner)
        {
        }

        public MelodicSlash(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 10 * (100 + boostAmount) / 100;
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
            });
        }
    }
}