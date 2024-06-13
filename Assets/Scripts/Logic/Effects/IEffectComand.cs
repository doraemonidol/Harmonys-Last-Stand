using Logic.Effects;

namespace Logic.Effects
{
    public abstract class EffectCommand
    {
        public int Handle { get; set; }
        
        protected int PreventActions = 0;

        protected int EffectEndTime;
        
        protected ICharacter Character;

        private EffectManager _effectManager;
        
        protected EffectCommand(ICharacter character)
        {
            Character = character;
        }

        public bool IsPreventing(int motion)
        {
            // using bitwise to check if motion is in PreventActions
            return (PreventActions & (1 << motion)) != 0;
        }

        public abstract void Execute();
        
        public void NotifyWhenEnd()
        {
            // Refresh the effect manager
            _effectManager.Refresh();
        }
    }
}