using System.Collections.Generic;

namespace Logic.Effects
{
    public class KnockBack : EffectCommand
    {
        public KnockBack(ICharacter character) : base(character)
        {
        }

        public KnockBack(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public KnockBack(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
        }

        public override void Execute()
        {
            
        }
    }
}