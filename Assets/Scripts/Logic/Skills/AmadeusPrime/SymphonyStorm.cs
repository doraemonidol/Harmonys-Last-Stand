using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.AmadeusPrime
{
    public class SymphonyStorm : AcSkill
    {
        public SymphonyStorm(Weapon owner) : base(owner)
        {
        }

        public SymphonyStorm(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = 10,
                ["timeout"] = 5,
                ["exHp"] = 0,
                ["exAtkSpd"] = 0,
                ["exDmg"] = 0,
                ["exMSp"] = 50,
                ["exMana"] = 0,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            target.ReceiveEffect(EffectHandle.Exhausted, args);
            target.ReceiveEffect(EffectHandle.Hallucinate, args);
        }
    }
}