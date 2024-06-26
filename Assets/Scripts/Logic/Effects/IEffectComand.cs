using System.Collections.Generic;
using Logic.Effects;

namespace Logic.Effects
{
    public abstract class EffectCommand
    {
        public int Handle { get; set; }
        
        protected int PreventActions = 0;

        protected long EffectEndTime;
        
        protected ICharacter Character;

        private EffectManager _effectManager;
        
        protected EffectCommand(ICharacter character)
        {
            Character = character;
        }

        protected EffectCommand(ICharacter character, int timeout) : this(character)
        {
            EffectEndTime = System.DateTime.Now.AddMilliseconds(timeout).Millisecond;
        }

        protected EffectCommand(
            ICharacter character, 
            int timeout, 
            Dictionary<string, int> fur_args
        ) : this(character, timeout)
        {
            
        }

        public bool IsPreventing(int motion)
        {
            // using bitwise to check if motion is in PreventActions
            return (PreventActions & (1 << motion)) != 0;
        }

        public abstract void Execute();

        public  bool IsExpired()
        {
            return System.DateTime.Now.Millisecond >= EffectEndTime;
        }
        
        public void NotifyWhenEnd()
        {
            // Refresh the effect manager
            _effectManager.Refresh();
        }
    }
}