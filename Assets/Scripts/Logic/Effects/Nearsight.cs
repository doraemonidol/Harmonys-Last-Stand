using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{ 
    public class Nearsight : EffectCommand
    {
        private int _nearsightValue = 0; // out of 100
        public Nearsight(ICharacter character) : base(character)
        {
        }

        public Nearsight(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Nearsight(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            _nearsightValue = furArgs["nearsightValue"];
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                // Character.ReceiveEffect(EffectHandle.Nearsight, new EventDto
                // {
                //     ["ev"] = "nearsight",
                //     ["nearsightValue"] = _nearsightValue
                // });
            
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableNearsight);
            
                NotifyWhenEnd();
            });
        }
    }
}