using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Bleeding : EffectCommand
    {
        private readonly int _hpDrain = 15;
        
        public Bleeding(ICharacter character) : base(character: character)
        {
            Handle = EffectHandle.Bleeding;
        }

        public Bleeding(ICharacter character, int timeout) : base(character: character, timeout: timeout)
        {
            Handle = EffectHandle.Bleeding;
        }

        public Bleeding(ICharacter character, int timeout, Dictionary<string, int> args) : base(character, timeout)
        {
            Handle = EffectHandle.Bleeding;
            _hpDrain = args[EffectHandle.HpDrain];
        }

        protected override void Update()
        {
            Character.UpdateEffect(EffectHandle.Bleeding, new EventDto
            {
                [EffectHandle.HpDrain] = _hpDrain
            });
        }
        
        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableBleeding);
        }
    }
}