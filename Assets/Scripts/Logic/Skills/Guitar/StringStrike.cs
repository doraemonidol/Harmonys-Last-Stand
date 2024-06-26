using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class StringStrike : AcSkill
    {
        public StringStrike(IWeapon owner) : base(owner)
        {
        }

        public StringStrike(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["dmg"] = 30,
                ["timeout"] = 10000,
            };
            target.ReceiveEffect(EffectHandle.Jinxed, args);
        }
    }
}