using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class NocturneEmbrace : AcSkill
    {
        public NocturneEmbrace(Weapon owner) : base(owner)
        {
        }

        public NocturneEmbrace(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate()
        {
            base.Activate();
            User.ReceiveEffect(EffectHandle.Healing, new EventDto
            {
                ["timeout"] = 5,
                ["HpHeal"] = 50,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}