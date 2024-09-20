using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Fear : EffectCommand
    {
        public Fear(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Fear;
        }

        public Fear(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Fear;
        }

        public Fear(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Fear;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableFear);
        }
    }
}