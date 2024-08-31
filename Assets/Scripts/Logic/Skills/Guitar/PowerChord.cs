using Common;
using Common.Context;
using DTO;
using Logic.Effects;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class PowerChord : AcSkill
    {
        public PowerChord(Weapon owner) : base(owner)
        {
        }

        public PowerChord(Weapon owner, long coolDownTime) : 
            base(owner, coolDownTime)
        {
            
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 25 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = finalDmg,
                ["timeout"] = 2,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Stunt, args);
        }
    }
}