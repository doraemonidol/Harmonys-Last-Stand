using DTO;
using Logic.Weapons;

namespace Logic.Skills.Violin
{
    public class CresendoBurst : AcSkill
    {
        public CresendoBurst(IWeapon owner) : base(owner)
        {
        }

        public CresendoBurst(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new System.NotImplementedException();
        }
    }
}