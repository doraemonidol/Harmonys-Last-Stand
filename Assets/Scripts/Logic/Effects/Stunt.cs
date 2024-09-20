using System.Threading;
using Logic.Helper;

namespace Logic.Effects
{
    public class Stunt : EffectCommand
    {
        public Stunt(ICharacter character) : base(character)
        {this.Handle = EffectHandle.Stunt;
        }

        public Stunt(ICharacter character, int timeout) : base(character, timeout)
        {
            this.Handle = EffectHandle.Stunt;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableStunt);
        }
    }
}