using System.Collections.Generic;
using Logic.Skills;
using Logic.Weapons;

namespace Logic.Effects
{
    public class Exhaust : EffectCommand
    {
        public Exhaust(ICharacter character) : base(character)
        {
        }

        public Exhaust(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Exhaust(ICharacter character, int timeout, Dictionary<string, int> furArgs) : 
            base(character, timeout, furArgs)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}