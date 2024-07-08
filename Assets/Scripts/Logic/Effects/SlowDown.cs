using System.Collections.Generic;

namespace Logic.Effects
{
    public class SlowDown : EffectCommand
    {
        public SlowDown(ICharacter character) : base(character)
        {
        }

        public SlowDown(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public SlowDown(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
        }

        public override void Execute()
        {
        }
    }
}