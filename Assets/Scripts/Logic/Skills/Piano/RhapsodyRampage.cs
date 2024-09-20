using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Piano
{
    public class RhapsodyRampage : AcSkill
    {
        public RhapsodyRampage(Weapon owner) : base(owner)
        {
        }

        public RhapsodyRampage(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var finalDmg = 20 * (100 + boostAmount) / 100;
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 20
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
        }
    }
}