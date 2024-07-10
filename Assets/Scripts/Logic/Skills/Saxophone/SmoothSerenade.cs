using System;
using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Weapons;
using UnityEditor;

namespace Logic.Skills.Saxophone
{
    public class SmoothSerenade : AcSkill
    {
        public SmoothSerenade(Weapon owner) : base(owner)
        {
        }

        public SmoothSerenade(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            ((IMainCharacter)User).ReceiveEffect(EffectHandle.Healing, new EventDto
            {
                [EffectHandle.HpGain] = 5,
                ["timeout"] = 10,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new NotImplementedException();
        }
    }
}