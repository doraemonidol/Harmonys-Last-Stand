using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Flute
{
    public class TranquilTune : AcSkill
    {
        public TranquilTune(IWeapon owner) : base(owner)
        {
        }

        public TranquilTune(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["dmg"] = 0,
                ["hp"] = 0,
                ["mana"] = 0,
                ["timeout"] = 10,
            };
            target.ReceiveEffect(EffectHandle.Exhausted, args);
        }
    }
}