using System.Threading;
using DTO;
using Logic.Helper;
using Logic.Villains;
using Logic.Weapons;

namespace Logic.Skills.MaestroMachina
{
    public class PhantomStab : AcSkill
    {
        private Thread _inProcessThread;
        
        public PhantomStab(Weapon owner) : base(owner)
        {
        }

        public PhantomStab(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        private void Update()
        {
            var next10Seconds = Time.WhatIsIt() + 10000;
            _inProcessThread = new Thread(() =>
            {
                while (Time.WhatIsIt() < next10Seconds)
                {
                    Thread.Sleep(500);
                }
                
                ((Villain)User).NotifySubscribers(new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "End_of_time_Skill_1",
                    }
                });
            });
            _inProcessThread.Start();
        }

        public override void Activate(ICharacter activator)
        {
            base.Activate(activator);
            Update();
        }

        public override void Cancel()
        {
            if (_inProcessThread.IsAlive)
                _inProcessThread.Abort();
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var eventd = new EventDto
            {
                [EffectHandle.HpReduce] = 50,
                [EffectHandle.HpDrain] = 10,
                ["timeout"] = 10,
            };
            target.ReceiveEffect(EffectHandle.GetHit, eventd);
            target.ReceiveEffect(EffectHandle.Bleeding, eventd);
        }
    }
}