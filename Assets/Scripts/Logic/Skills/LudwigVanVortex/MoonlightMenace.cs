using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.LudwigVanVortex
{
    public class MoonlightMenace : AcSkill
    {
        public MoonlightMenace(Weapon owner) : base(owner)
        {
        }

        public MoonlightMenace(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
            
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 8,
            });
            target.ReceiveEffect(EffectHandle.Nearsight, new EventDto
            {
                ["timeout"] = 5,
            });
        }
    }
}