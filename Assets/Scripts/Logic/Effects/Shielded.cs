using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Shielded : EffectCommand
    {
        public Shielded(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Shielded;
        }

        public Shielded(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Shielded;
        }
        
        public Shielded(ICharacter character, int timeout, int shieldValue) : base(character, timeout)
        {
            Handle = EffectHandle.Shielded;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableShielded);
        }
    }
}