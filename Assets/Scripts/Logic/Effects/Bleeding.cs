using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Bleeding : EffectCommand
    {
        private readonly int _hpDrain = 15;
        
        public Bleeding(ICharacter character) : base(character: character)
        {
            PreventActions = 0;
            
            // Update EffectEndTime to the current time + 6 seconds using System
            EffectEndTime = System.DateTime.Now.AddMilliseconds(6000).Millisecond;

            Handle = EffectHandle.Bleeding;
        }

        public Bleeding(ICharacter character, int timeout) : base(character: character, timeout: timeout)
        {
            PreventActions = 0;

            EffectEndTime = System.DateTime.Now.AddMilliseconds(timeout).Millisecond;
            
            Handle = EffectHandle.Bleeding;
        }

        public Bleeding(ICharacter character, int timeout, Dictionary<string, int> args) : base(character, timeout)
        {
            _hpDrain = args[EffectHandle.HpDrain];
        }

        public override void Execute()
        {
            var args = new EventDto
            {
                [EffectHandle.HpDrain] = _hpDrain
            };
            // Open an thread
            var thread = new Thread(() =>
            {
                
                // While the current time is less than EffectEndTime
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Character.UpdateEffect(EffectHandle.Bleeding, args);
                    
                    // Sleep for 1 second
                    Thread.Sleep(1000);
                }
                
                // Notify the Effect Manager when the effect ends
                NotifyWhenEnd();
            });
        }
    }
}