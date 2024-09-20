using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Sleepy : EffectCommand
    {
        private Thread _thread;
        
        public Sleepy(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Sleepy;
        }

        public Sleepy(ICharacter character, int timeout) : base(character, timeout)
        {
            
            Handle = EffectHandle.Sleepy;
        }

        public Sleepy(ICharacter character, int timeout, Dictionary<string, int> fur_args) : base(character, timeout, fur_args)
        {
            Handle = EffectHandle.Sleepy;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableSleepy);
        }
        
        public void Cancel()
        {
            _thread.Abort();
        }
    }
}