using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Troop
{
    public class NormalAttack : AcSkill
    {
        public NormalAttack(Weapon owner) : base(owner)
        {
        }

        public NormalAttack(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = 10,
            });
        }
    }
}