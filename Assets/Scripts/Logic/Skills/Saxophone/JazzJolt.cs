using Common;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Saxophone
{
    public class JazzJolt : AcSkill
    {
        public JazzJolt(Weapon owner) : base(owner)
        {
        }

        public JazzJolt(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            // Deals 15 HP damage and root enemies in a 3-meter radius for 1 seconds.
            
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 15 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 15,
                ["timeout"] = 1 * GameStats.BASE_TIME_UNIT,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Rooted, args);
        }
    }
}