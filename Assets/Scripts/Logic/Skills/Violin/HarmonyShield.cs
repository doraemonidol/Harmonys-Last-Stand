using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Weapons;
using UnityEngine;

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

        public override void Activate(ICharacter activator)
        {
            Debug.Log("Activated Harmony Shiled");
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Shielded, new EventDto
            {
                ["timeout"] = 10,
                ["absort"] = 70,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
        }
    }
}