using System;
using System.Collections;

namespace Logic.Effects
{
    public class EffectManager
    {
        private readonly ArrayList _effects = ArrayList.Synchronized(new ArrayList());
        
        private readonly object _lock = new object();
        
        // Assume that there are 32 effects in total.
        // private int _effectRoll = 0;

        private ArrayList _effectRoll = new ArrayList(32);

        public EffectManager()
        {
            for (var _ = 0; _ < 32; _++)
            {
                _effectRoll.Add(0);
            }
        }
        
        public void Refresh()
        {
            lock (_lock)
            {
                foreach (EffectCommand effect in _effects)
                {
                    if (effect.IsExpired())
                    {
                        _effects.Remove(effect);
                    }
                }

                foreach (EffectCommand effect in _effects)
                {
                    _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] + 1;
                }
            }
        }
        
        public void Add(EffectCommand effect)
        {
            _effects.Add(effect);
            _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] + 1;
        }

        public void Remove(EffectCommand effect)
        {
            _effects.Remove(effect);
            _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] - 1;
        }
        
        public bool CheckIfEffectApply(int effect)
        {
            return (int)_effectRoll[effect] > 0;
        }
    }
}