using DTO;
using Logic.Helper;
using Logic.Weapons;
using UnityEditor;

namespace Logic.Skills.Saxophone
{
    public class SmoothSerenade : AcSkill
    {
        public SmoothSerenade(IWeapon owner) : base(owner)
        {
        }

        public SmoothSerenade(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpGain] = 5,
                ["timeout"] = 10,
            };
            attacker.ReceiveEffect(EffectHandle.Healing, args);
        }
    }
}