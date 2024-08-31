using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.LudwigVanVortex
{
    public class TheSkiesDescend : AcSkill
    {
        public TheSkiesDescend(Weapon owner) : base(owner)
        {
        }

        public TheSkiesDescend(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var isInside = (bool)context["range"];
            var dmg = isInside ? 40 : 10;
            var effect = isInside ? EffectHandle.Stunt : EffectHandle.Exhausted;
            var timeout = isInside ? 3 : 4;
            target.ReceiveEffect(EffectHandle.GetHit, new EventDto
            {
                [EffectHandle.HpReduce] = dmg,
            });
            target.ReceiveEffect(effect, new EventDto
            {
                ["timeout"] = timeout,
                ["exHp"] = 0,
                ["exMSp"] = 50,
                ["exAtkSpd"] = 0,
                ["exMana"] = 0,
                ["exDmg"] = 0,
            });
        }
    }
}