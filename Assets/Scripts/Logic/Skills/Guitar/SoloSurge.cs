using Common;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Guitar
{
    public class SoloSurge : AcSkill
    {
        public SoloSurge(Weapon owner) : base(owner)
        {
        }

        public SoloSurge(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            var args = new EventDto
            {
                ["boostHp"] = 0,
                ["boostMSp"] = 0,
                ["boostAtkSpd"] = 0,
                ["boostMana"] = 0,
                ["boostDmg"] = 30,
                ["timeout"] = 10,
            }; 
            User.ReceiveEffect(EffectHandle.Jinxed, args);
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            
        }
    }
}