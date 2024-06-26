using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class AllegroAgility : AcSkill
    {
        public AllegroAgility(IWeapon owner) : base(owner)
        {
        }

        public AllegroAgility(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                ["dmp"] = 0,
                ["mana"] = 0,
                ["movspd"] = 0,
                ["atkspd"] = 0,
            };
            attacker.ReceiveEffect(EffectHandle.Jinxed, args);
        }
    }
}