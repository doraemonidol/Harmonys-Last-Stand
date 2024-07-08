using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Resonance : EffectCommand
    {
        public Resonance(ICharacter character) : base(character)
        {
        }

        public Resonance(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Resonance(ICharacter character, int timeout, Dictionary<string, int> furArgs) 
            : base(character, timeout, furArgs)
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
                
                Character.ReceiveEffect(EffectHandle.DisableResonance);
                
                NotifyWhenEnd();
            });
        }
    }
}