using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class VoidCancellation : AcSkill
    {
        public VoidCancellation(Weapon owner) : base(owner)
        {
        }

        public VoidCancellation(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Void, new EventDto
            {
                ["timeout"] = 10000,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}