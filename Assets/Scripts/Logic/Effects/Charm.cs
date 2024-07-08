using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Charm : EffectCommand
    {
        public Charm(ICharacter character) : base(character)
        {
        }

        public Charm(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Charm(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                // Character.UpdateEffect(EffectHandle.Charm, new EventDto
                // {
                //     ["ev"] = "charm",
                // });
                
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableCharm);
                
                NotifyWhenEnd();
            });
        }
    }
}