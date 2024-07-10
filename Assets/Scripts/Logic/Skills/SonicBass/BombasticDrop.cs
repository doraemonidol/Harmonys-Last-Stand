using System.Collections.Generic;
using Common.Context;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.SonicBass
{
    public class BombasticDrop : AcSkill
    {
        public BombasticDrop(Weapon owner) : base(owner)
        {
        }

        public BombasticDrop(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var posAttacker = new KeyValuePair<float, float>
            (
                (float) context["x1"], 
                (float) context["y1"]
            );
            var posTarget = new KeyValuePair<float, float>
            (
                (float) context["x2"], 
                (float) context["y2"]
            );
            var distance = Utils.GetDistance(posAttacker, posTarget);
            var boostAmount = GameContext.GetInstance().Get("dmg+");
            var dmgExp = distance switch
            {
                < 1 => 70 * (100 + boostAmount) / 100,
                < 2 => 50 * (100 + boostAmount) / 100,
                < 3 => 30 * (100 + boostAmount) / 100,
                <= 4 => 10 * (100 + boostAmount) / 100,
                _ => 0,
            };
            var args = new EventDto
            {
                [EffectHandle.HpReduce] = dmgExp,
                ["timeout"] = 3,
            };
            target.ReceiveEffect(EffectHandle.GetHit, args);
            if (!target.IsEffectApplied(EffectHandle.Resonance)) return;
            target.ReceiveEffect(EffectHandle.Stunt, args);
        }
    }
}