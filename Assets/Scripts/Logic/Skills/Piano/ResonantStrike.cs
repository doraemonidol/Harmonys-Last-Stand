using Common;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class ResonantStrike : AcSkill
    {
        public ResonantStrike(Weapon owner) : base(owner)
        {
        }

        public ResonantStrike(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 20 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
                ["timeout"] = (int)0.5 * GameStats.BASE_TIME_UNIT,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Stunt, args);
        }
    }
}