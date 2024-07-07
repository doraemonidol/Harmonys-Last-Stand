using DTO;
using Logic.Effects;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class PowerChord : AcSkill
    {
        public PowerChord(Weapon owner) : base(owner)
        {
        }

        public PowerChord(Weapon owner, long coolDownTime) : 
            base(owner, coolDownTime)
        {
            
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpDrain] = 27
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}