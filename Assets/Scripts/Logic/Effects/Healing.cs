using System.Collections.Generic;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Healing : EffectCommand
    {
        private int _healingAmount;

        public Healing(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Healing;
        }

        public Healing(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Healing;
        }

        public Healing(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Healing;
            _healingAmount = furArgs["boostHp"];
        }
        
        protected override void Update()
        {
            Character.UpdateEffect(EffectHandle.Healing, new EventDto
            {
                [EffectHandle.HpGain] = _healingAmount
            });
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableHealing);
        }
    }
}