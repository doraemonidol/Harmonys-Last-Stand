using DTO;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class EchoingWaltz : AcSkill
    {
        public EchoingWaltz(IWeapon owner) : base(owner)
        {
        }

        public EchoingWaltz(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new System.NotImplementedException();
        }
    }
}