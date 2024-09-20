using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class CresendoBurst : AcSkill
    {
        public CresendoBurst(Weapon owner) : base(owner)
        {
        }

        public CresendoBurst(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 15 * (100 + boostAmount) / 100;
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 15,
            });
        }
    }
}