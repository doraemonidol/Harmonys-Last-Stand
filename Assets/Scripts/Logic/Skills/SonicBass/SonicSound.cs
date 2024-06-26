using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class SonicSound : AcSkill
    {
        public SonicSound(IWeapon owner) : base(owner)
        {
        }

        public SonicSound(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 20,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}