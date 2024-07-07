using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Flute
{
    public class WhistlingWind : AcSkill
    {
        public WhistlingWind(Weapon owner) : base(owner)
        {
        }

        public WhistlingWind(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = EffectHandle.MysticMelodyDmg
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}