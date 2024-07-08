using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class SoloSurge : AcSkill
    {
        public SoloSurge(Weapon owner) : base(owner)
        {
        }

        public SoloSurge(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 35
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}