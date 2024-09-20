using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class StringStrike : AcSkill
    {
        public StringStrike(Weapon owner) : base(owner)
        {
        }

        public StringStrike(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 27 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpDrain] = finalDmg,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}