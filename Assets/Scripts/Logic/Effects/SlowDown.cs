using System.Collections.Generic;
using Logic.Helper;

namespace Logic.Effects
{
    public class SlowDown : EffectCommand
    {
        public SlowDown(ICharacter character) : base(character)
        {
            Handle = EffectHandle.SlowDown;
        }

        public SlowDown(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.SlowDown;
        }

        public SlowDown(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.SlowDown;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableSlowDown);
        }

        public override void Execute()
        {
        }
    }
}