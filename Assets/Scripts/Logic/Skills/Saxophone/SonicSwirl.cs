using System.Threading;
using DTO;
using Logic.Helper;
using Logic.Weapons;

namespace Logic.Skills.Saxophone
{
    public class SonicSwirl : AcSkill
    {
        public SonicSwirl(IWeapon owner) : base(owner)
        {
        }

        public SonicSwirl(IWeapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            // Deals 20 HP damage per second to enemies within a 4-meter radius for 6 seconds.
            
            var thread = new Thread(() =>
            {
                for (var i = 0; i < 6; i++)
                {
                    var args = new EventDto
                    {
                        [EffectHandle.HpReduce] = 20,
                    };
                    target.ReceiveEffect(EffectHandle.GetHit, args);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}