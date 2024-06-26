using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }
    }
}