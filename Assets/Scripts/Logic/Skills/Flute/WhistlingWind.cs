using Common.Context;
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
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 10 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                ["timeout"] = finalDmg,
            };
        }
    }
}