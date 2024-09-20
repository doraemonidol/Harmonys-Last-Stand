using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{ 
    public class Nearsight : EffectCommand
    {
        public Nearsight(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Nearsight;
        }

        public Nearsight(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Nearsight;
        }

        public Nearsight(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Nearsight;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableNearsight);
        }
    }
}