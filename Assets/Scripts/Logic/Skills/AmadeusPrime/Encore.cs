using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.AmadeusPrime
{
    public class Encore : AcSkill
    {
        public Encore(Weapon owner) : base(owner)
        {
        }

        public Encore(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 30,
            });
            target.ReceiveEffect(EffectHandle.Charm, new EventDto
            {
                ["timeout"] = 4,
            });
        }
    }
}