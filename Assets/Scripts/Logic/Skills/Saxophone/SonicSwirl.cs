using System.Threading;
using Common;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Saxophone
{
    public class SonicSwirl : AcSkill
    {
        public SonicSwirl(Weapon owner) : base(owner)
        {
        }

        public SonicSwirl(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            // Deals 20 HP damage per second to enemies within a 4-meter radius for 6 seconds.
            
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 20 * (100 + boostAmount) / 100;
            
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
                ["timeout"] = 6 * GameStats.BASE_TIME_UNIT,
            };
            
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}