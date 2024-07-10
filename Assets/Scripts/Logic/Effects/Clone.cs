using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons;
using UnityEngine.TextCore.Text;

namespace Logic.Effects
{
    public class Clone : EffectCommand
    {
        public Clone(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Clone;
        }

        public Clone(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Clone;
        }

        public Clone(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Clone;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableClone);
        }
    }
}