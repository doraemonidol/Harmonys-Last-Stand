using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Flute
{
    public class WhistlingWind : AcSkill
    {
        public WhistlingWind(Weapon owner) : base(owner)
        {
        }

        public WhistlingWind(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            var args = new EventDto
            {
                ["timeout"] = 10000,
            };
            User.ReceiveEffect(EffectHandle.Resistance, args);
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
        }
    }
}