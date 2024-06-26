using System.Collections.Generic;

namespace Logic.Effects
{
    public class Resistance : EffectCommand
    {
        public Resistance(ICharacter character) : base(character)
        {
        }

        public Resistance(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Resistance(ICharacter character, int timeout, Dictionary<string, int> fur_args) : base(character, timeout, fur_args)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}