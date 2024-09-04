using Common;
using DTO;
using Logic.Helper;
using Logic.Weapons;
using UnityEngine;

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
            // Debug.Log("Cooldown: " + this.CoolDownTime + " ms.");
            base.Activate(activator);
            Debug.Log("RequiemResurrection activated.");
            User.ReceiveEffect(EffectHandle.Resurrect);
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
        }
    }
}