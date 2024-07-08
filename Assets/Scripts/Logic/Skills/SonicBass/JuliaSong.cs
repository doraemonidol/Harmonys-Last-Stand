using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class JuliaSong : AcSkill
    {
        public JuliaSong(Weapon owner) : base(owner)
        {
        }

        public JuliaSong(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
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