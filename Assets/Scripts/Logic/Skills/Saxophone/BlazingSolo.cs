using Common;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Saxophone
{
    public class BlazingSolo : AcSkill
    {
        public BlazingSolo(Weapon owner) : base(owner)
        {
        }

        public BlazingSolo(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 10 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
                [EffectHandle.HpDrain] = 3,
                ["timeout"] = 10 * GameStats.BASE_TIME_UNIT,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Bleeding, args);
        }
    }
}