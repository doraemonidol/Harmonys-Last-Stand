using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class EchoingWaltz : AcSkill
    {
        public EchoingWaltz(Weapon owner) : base(owner)
        {
        }

        public EchoingWaltz(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }
        
        public void Activate(ICharacter activator)
        {
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Clone, new EventDto
            {
                ["timeout"] = 20000,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
        }
    }
}