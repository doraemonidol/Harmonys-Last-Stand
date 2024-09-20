using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Silent : EffectCommand
    {
        public Silent(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Silent;
        }

        public Silent(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Silent;
        }
        
        public Silent(ICharacter character, int timeout, int shieldValue) : base(character, timeout)
        {
            Handle = EffectHandle.Silent;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableSilent);
        }
    }
}