using DTO;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class HarmonyShield : AcSkill
    {
        public HarmonyShield(IWeapon owner) : base(owner)
        {
        }

        public HarmonyShield(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new System.NotImplementedException();
        }
    }
}