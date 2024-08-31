using Common;
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

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Healing, new EventDto
            {
                ["timeout"] = 10 * GameStats.BASE_TIME_UNIT,
                [EffectHandle.HpGain] = 5,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}