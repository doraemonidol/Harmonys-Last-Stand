using DTO;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class MelodicSlash : AcSkill
    {
        public MelodicSlash(IWeapon owner) : base(owner)
        {
        }

        public MelodicSlash(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new System.NotImplementedException();
        }
    }
}