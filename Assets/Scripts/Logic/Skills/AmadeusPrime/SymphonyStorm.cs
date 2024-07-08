using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.AmadeusPrime
{
    public class SymphonyStorm : AcSkill
    {
        public SymphonyStorm(Weapon owner) : base(owner)
        {
        }

        public SymphonyStorm(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                ["dmg"] = 10,
            });
            target.ReceiveEffect(EffectHandle.SlowDown, new EventDto
            {
                ["timeout"] = 5,
                ["stat"] = 50,
            });
        }
    }
}