using System.Collections;

namespace Logic.Effects
{
    public class EffectManager
    {
        private readonly ArrayList _effects = ArrayList.Synchronized(new ArrayList());
        
        private readonly object _lock = new object();

        private int _effectRoll = 0;
        
        public void Refresh()
        {
            
        }
        
        public void Add(EffectCommand effect)
        {
            _effects.Add(effect);
            _effectRoll |= (1 << effect.Handle);
        }

        public void Remove(EffectCommand effect)
        {
            _effects.Remove(effect);
        }
        
        public void CheckIfEffectApply(int effect)
        {
            
        }
    }
}