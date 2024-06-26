using DTO;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class VoidCancellation : AcSkill
    {
        public VoidCancellation(IWeapon owner) : base(owner)
        {
        }

        public VoidCancellation(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}