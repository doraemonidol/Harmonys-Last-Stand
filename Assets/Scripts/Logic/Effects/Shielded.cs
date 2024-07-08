using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Shielded : EffectCommand
    {
        public Shielded(ICharacter character) : base(character)
        {
            PreventActions |= (1 << MotionHandle.MotionGetAttacked);
            
            // Automatically timeout is 5 seconds.
            EffectEndTime = System.DateTime.Now.AddMilliseconds(5000).Millisecond;
            
            Handle = EffectHandle.Shielded;
        }

        public Shielded(ICharacter character, int timeout) : base(character, timeout)
        {
            PreventActions |= (1 << MotionHandle.MotionGetAttacked);
            
            EffectEndTime = System.DateTime.Now.AddMilliseconds(timeout).Millisecond;

            Handle = EffectHandle.Shielded;
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                // Character.ReceiveEffect(EffectHandle.Shielded);
                
                // While the current time is less than EffectEndTime
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    // Sleep for 1 second
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableShielded);
                
                // Notify the Effect Manager when the effect ends
                NotifyWhenEnd();
            });
        }
    }
}