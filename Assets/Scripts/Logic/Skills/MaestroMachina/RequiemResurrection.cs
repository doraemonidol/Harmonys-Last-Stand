using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.MaestroMachina
{
    public class RequiemResurrection : AcSkill
    {
        public RequiemResurrection(Weapon owner) : base(owner)
        {
        }

        public RequiemResurrection(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Resurrect);
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
        }
    }
}