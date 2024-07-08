using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class HarmonyShield : AcSkill
    {
        public HarmonyShield(Weapon owner) : base(owner)
        {
        }

        public HarmonyShield(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate()
        {
            base.Activate();
            User.ReceiveEffect(EffectHandle.Shielded, new EventDto
            {
                ["timeout"] = 10,
                ["absort"] = 70,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new System.NotImplementedException();
        }
    }
}