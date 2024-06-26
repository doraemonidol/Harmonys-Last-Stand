using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class RhapsodyRampage : AcSkill
    {
        public RhapsodyRampage(IWeapon owner) : base(owner)
        {
        }

        public RhapsodyRampage(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 20
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}