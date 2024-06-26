using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class JuliaSong : AcSkill
    {
        public JuliaSong(IWeapon owner) : base(owner)
        {
        }

        public JuliaSong(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["timeout"] = 10,
            };
            target.ReceiveEffect(EffectHandle.Resonance, args);
        }
    }
}