using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class NocturneEmbrace : AcSkill
    {
        public NocturneEmbrace(IWeapon owner) : base(owner)
        {
        }

        public NocturneEmbrace(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["timeout"] = 5,
                ["HpHeal"] = 50,
            };
            attacker.ReceiveEffect(EffectHandle.Healing, args);
        }
    }
}