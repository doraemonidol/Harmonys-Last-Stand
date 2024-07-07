using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class EchoingWaltz : AcSkill
    {
        public EchoingWaltz(Weapon owner) : base(owner)
        {
        }

        public EchoingWaltz(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 25,
            });
        }
    }
}