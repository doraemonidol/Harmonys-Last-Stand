using System.Collections.Generic;
using Logic.Helper;

namespace Logic.Effects
{
    public class KnockBack : EffectCommand
    {
        public KnockBack(ICharacter character) : base(character)
        {
            Handle = EffectHandle.KnockBack;
        }

        public KnockBack(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.KnockBack;
        }

        public KnockBack(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.KnockBack;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableKnockBack);
        }

        public override void Execute()
        {
            
        }
    }
}