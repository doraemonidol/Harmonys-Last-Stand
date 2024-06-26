using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Silent : EffectCommand
    {
        public Silent(ICharacter character) : base(character)
        {
            PreventActions |= (1 << MotionHandle.MotionAttack);
            
            // Automatically timeout is 5 seconds.
            EffectEndTime = System.DateTime.Now.AddMilliseconds(5000).Millisecond;
            
            Handle = EffectHandle.Silent;
        }

        public Silent(ICharacter character, int timeout) : base(character, timeout)
        {
            PreventActions |= (1 << MotionHandle.MotionAttack);
            
            EffectEndTime = System.DateTime.Now.AddMilliseconds(timeout).Millisecond;

            Handle = EffectHandle.Silent;
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                
                // While the current time is less than EffectEndTime
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    // Sleep for 1 second
                    Thread.Sleep(1000);
                }
                
                // Notify the Effect Manager when the effect ends
                NotifyWhenEnd();
            });
        }
    }
}