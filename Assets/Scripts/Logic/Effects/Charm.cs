using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Charm : EffectCommand
    {
        public Charm(ICharacter character) : base(character)
        {
            this.Handle = EffectHandle.Charm;
        }

        public Charm(ICharacter character, int timeout) : base(character, timeout)
        {
            this.Handle = EffectHandle.Charm;
        }

        public Charm(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            this.Handle = EffectHandle.Charm;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableCharm);
        }
    }
}