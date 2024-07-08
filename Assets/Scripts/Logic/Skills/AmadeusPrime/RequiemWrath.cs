using DTO;
using Logic.Helper;
using Logic.Weapons;
using UnityEngine;

namespace Logic.Skills.AmadeusPrime
{
    public class RequiemWrath : AcSkill
    {
        public RequiemWrath(Weapon owner) : base(owner)
        {
            
        }

        public RequiemWrath(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
            
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["dmg"] = 15,
                ["timeout"] = 3,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Stunt, args);
        }
    }
}