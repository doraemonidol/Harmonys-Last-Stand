using System;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.MaestroMachina
{
    public class JackInTheBoxMayhem : AcSkill
    {
        public JackInTheBoxMayhem(Weapon owner) : base(owner)
        {
        }

        public JackInTheBoxMayhem(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var randomNumber = new Random().Next(0, 10);

            if (randomNumber % 2 != 0) return;
            
            var eventd = new EventDto
            {
                [EffectHandle.HpReduce] = 40,
                ["timeout"] = 5,
                ["dmg"] = 20,
                ["movspd"] = 0,
                ["atkspd"] = 0,
                ["mana"] = 0,
                ["hp"] = 0,
            };
            target.ReceiveEffect(EffectHandle.GetHit, eventd);
            target.ReceiveEffect(EffectHandle.Exhausted, eventd);
        }
    }
}