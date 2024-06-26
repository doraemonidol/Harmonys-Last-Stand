using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }
    }
}