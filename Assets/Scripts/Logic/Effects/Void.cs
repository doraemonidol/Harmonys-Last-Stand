using System.Collections.Generic;
using Logic.Helper;

namespace Logic.Effects
{
    public class Void : EffectCommand
    {
        public Void(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Void;
        }

        public Void(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Void;
        }

        public Void(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Void;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableVoid);
        }
    }
}