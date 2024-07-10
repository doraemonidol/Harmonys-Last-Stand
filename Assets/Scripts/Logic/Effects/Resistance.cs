using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Resistance : EffectCommand
    {
        public Resistance(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Resistance;
        }

        public Resistance(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Resistance;
        }

        public Resistance(ICharacter character, int timeout, Dictionary<string, int> fur_args) : base(character, timeout, fur_args)
        {
            Handle = EffectHandle.Resistance;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableResistance);
        }
    }
}