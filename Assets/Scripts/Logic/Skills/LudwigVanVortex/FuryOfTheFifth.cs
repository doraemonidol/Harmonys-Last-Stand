using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.LudwigVanVortex
{
    public class FuryOfTheFifth : AcSkill
    {
        public FuryOfTheFifth(Weapon owner) : base(owner)
        {
        }

        public FuryOfTheFifth(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 20,
            });
            target.ReceiveEffect(EffectHandle.Fear, new EventDto
            {
                ["timeout"] = 2, 
            });
        }
    }
}