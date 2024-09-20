using System.Collections.Generic;
using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Hallucination : EffectCommand
    {
        public Hallucination(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Hallucinate;
        }

        public Hallucination(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Hallucinate;
        }

        public Hallucination(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Hallucinate;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableHallucinate);
        }
    }
}