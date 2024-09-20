using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Resonance : EffectCommand
    {
        public Resonance(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Resonance;
        }

        public Resonance(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Resonance;
        }

        public Resonance(ICharacter character, int timeout, Dictionary<string, int> furArgs) 
            : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Resonance;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableResonance);
        }
    }
}