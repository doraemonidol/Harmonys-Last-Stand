using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Stunt : EffectCommand
    {
        public Stunt(ICharacter character) : base(character)
        {
            PreventActions |= (1 << MotionHandle.MotionAttack);
            PreventActions |= (1 << MotionHandle.MotionRun);
            
            // Automatically timeout is 5 seconds.
            this.EffectEndTime = System.DateTime.Now.AddMilliseconds(5000).Millisecond;
            
            this.Handle = EffectHandle.Stunt;
        }

        public Stunt(ICharacter character, int timeout) : base(character, timeout)
        {
            PreventActions |= (1 << MotionHandle.MotionAttack);
            PreventActions |= (1 << MotionHandle.MotionRun);
            
            this.EffectEndTime = System.DateTime.Now.AddMilliseconds(timeout).Millisecond;

            this.Handle = EffectHandle.Stunt;
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