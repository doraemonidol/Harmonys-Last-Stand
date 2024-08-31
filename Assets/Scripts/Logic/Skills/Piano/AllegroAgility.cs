using System;
using Common;
using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class AllegroAgility : AcSkill
    {
        public AllegroAgility(Weapon owner) : base(owner)
        {
        }

        public AllegroAgility(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            User.ReceiveEffect(EffectHandle.Jinxed, new EventDto
            {
                ["dmp"] = 0,
                ["mana"] = 0,
                ["movspd"] = 30,
                ["atkspd"] = 20,
                ["hp"] = 0,
                ["timeout"] = 10 * GameStats.BASE_TIME_UNIT,
            });
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            throw new NotImplementedException();
        }
    }
}