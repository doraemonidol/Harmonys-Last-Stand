using System;
using System.Threading;
using DTO;
using Logic.Weapons;

namespace Logic.Skills
{
    public abstract class AcSkill : LogicObject, ISkill
    {
        protected IWeapon Owner;
        
        protected long NextTimeToAvailable { get; set; }
        
        protected long CoolDownTime { get; set; }
        
        protected bool Locked = false;
        
        protected AcSkill(IWeapon owner)
        {
            this.Owner = owner;
            this.CoolDownTime = 5000;
        }

        protected AcSkill(IWeapon owner, long coolDownTime)
        {
            this.Owner = owner;
            this.CoolDownTime = coolDownTime;
        }

        public bool IsAvailable()
        {
            return (!Locked) && (DateTime.Now.Millisecond >= NextTimeToAvailable);
        }

        public abstract void Affect(ICharacter attacker, ICharacter target, EventDto context);

        public void Activate()
        {
            if (this.Locked)
            {
                throw new Exception("Exception thrown when trying to activate a locked skill");
            }
            if (this.NextTimeToAvailable > DateTime.Now.Ticks)
            {
                throw new Exception("Exception thrown when trying to activate a skill that is not available");
            }
            this.NextTimeToAvailable = System.DateTime.Now.AddMilliseconds(CoolDownTime).Millisecond;
            this.Lock();
        }
        
        private void CallVisitor(string typeEv)
        {
            var visitor = new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = typeEv
                }
            };
            this.NotifySubscribers(visitor);
        }

        public void Lock()
        {
            this.Locked = true;
            CallVisitor("lock");
            var thread = new Thread(() =>
            {
                while (DateTime.Now.Millisecond < this.NextTimeToAvailable)
                {
                    Thread.Sleep(500);
                }
                this.Unlock();
            });
        }

        public void Unlock()
        {
            this.Locked = false;
            CallVisitor("unlock");
        }
    }
}