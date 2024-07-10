using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Rooted : EffectCommand
    {
        public Rooted(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Rooted;
        }

        public Rooted(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Rooted;
        }
        
        public Rooted(ICharacter character, int timeout, int shieldValue) : base(character, timeout)
        {
            Handle = EffectHandle.Rooted;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableRooted);
        }
    }
}