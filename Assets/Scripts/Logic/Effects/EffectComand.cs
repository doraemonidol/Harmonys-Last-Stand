using System.Collections.Generic;
using System.Threading;
using Logic.Effects;
using Logic.Helper;
using UnityEngine;

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
            EffectEndTime = CustomTime.WhatIsIt() + 5000;
        }

        protected EffectCommand(ICharacter character, int timeout) : this(character)
        {
            EffectEndTime = CustomTime.WhatIsIt() + timeout;
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
            Debug.Log("EffectCommand.Execute() StartTime: " + CustomTime.WhatIsIt());
            Debug.Log("EffectCommand.Execute()" + EffectEndTime);
            var thread = new Thread(() =>
            {
                
                while (CustomTime.WhatIsIt() < EffectEndTime)
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
            var currentTime = CustomTime.WhatIsIt();
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