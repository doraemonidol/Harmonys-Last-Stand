using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Sleepy : EffectCommand
    {
        public Sleepy(ICharacter character) : base(character)
        {
        }

        public Sleepy(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Sleepy(ICharacter character, int timeout, Dictionary<string, int> fur_args) : base(character, timeout, fur_args)
        {
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableSleepy);
                
                NotifyWhenEnd();
            });
        }
    }
}