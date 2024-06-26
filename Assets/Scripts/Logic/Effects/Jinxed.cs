using System.Collections.Generic;

namespace Logic.Effects
{
    public class Jinxed : EffectCommand
    {
        public Jinxed(ICharacter character) : base(character)
        {
        }

        public Jinxed(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Jinxed(ICharacter character, int timeout, Dictionary<string, int> fur_args) : base(character, timeout, fur_args)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}