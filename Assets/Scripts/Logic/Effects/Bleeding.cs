using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Bleeding : EffectCommand
    {
        public Bleeding(ICharacter character) : base(character: character)
        {
            PreventActions = 0;
            
            // Update EffectEndTime to the current time + 6 seconds using System
            EffectEndTime = System.DateTime.Now.AddSeconds(6).Second;

            Handle = EffectHandle.Bleeding;
        }

        public override void Execute()
        {
            // Open an thread
            var thread = new Thread(() =>
            {
                // While the current time is less than EffectEndTime
                while (System.DateTime.Now.Second < EffectEndTime)
                {
                    Character.ReceiveEffect(EffectHandle.Bleeding);
                    
                    // Sleep for 1 second
                    Thread.Sleep(1000);
                }
            });
        }
    }
}