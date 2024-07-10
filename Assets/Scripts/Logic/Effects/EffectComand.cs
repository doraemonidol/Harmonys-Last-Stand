using System.Collections.Generic;
using System.Threading;
using Logic.Effects;
using Logic.Helper;

namespace Logic.Effects
{
    public abstract class EffectCommand
    {
        public int Handle { get; set; }

        protected long EffectEndTime;
        
        protected ICharacter Character;

        private EffectManager _effectManager;
        
        protected EffectCommand(ICharacter character)
        {
            Character = character;
            EffectEndTime = Time.WhatIsIt() + 5000;
        }

        protected EffectCommand(ICharacter character, int timeout) : this(character)
        {
            EffectEndTime = Time.WhatIsIt() + timeout;
        }

        protected EffectCommand(
            ICharacter character, 
            int timeout, 
            Dictionary<string, int> furArgs
        ) : this(character, timeout)
        {
            
        }

        protected virtual void Update()
        {
            
        }

        protected abstract void Disable();

        public virtual void Execute()
        {
            var thread = new Thread(() =>
            {
                
                while (Time.WhatIsIt() < EffectEndTime)
                {
                    this.Update();
                    
                    Thread.Sleep(1000);
                }
                
                this.Disable();
                
                NotifyWhenEnd();
            });
            
            thread.Start();
        }

        public  bool IsExpired()
        {
            var currentTime = Time.WhatIsIt();
            return currentTime >= EffectEndTime;
        }
        
        public void NotifyWhenEnd()
        {
            // Refresh the effect manager
            _effectManager.Refresh(Handle);
        }

        public void GetManagedBy(EffectManager effectManager)
        {
            _effectManager = effectManager;
        }
        
        public void StopGetManaged()
        {
            _effectManager = null;
        }
    }
}