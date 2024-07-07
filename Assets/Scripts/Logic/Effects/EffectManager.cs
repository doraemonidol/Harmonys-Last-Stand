using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Helper;

namespace Logic.Effects
{
    public class EffectManager
    {
        private readonly Dictionary<int, ArrayList> _effects = new();
        
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
        
        public void Refresh(int effectHandle)
        {
            lock (_lock)
            {
                foreach (var effect in _effects[effectHandle])
                {
                    if (((EffectCommand)effect).IsExpired())
                    {
                        Remove((EffectCommand)effect);
                    }
                }
            }
        }
        
        public void Add(EffectCommand effect)
        {
            lock (_lock)
            {
                _effects[effect.Handle].Add(effect);
                _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] + 1;
            }
        }

        public void Erase(int effectHandle)
        {
            lock (_lock)
            {
                _effects[effectHandle].Clear();
                _effectRoll[effectHandle] = 0;
            }
        }

        public void Remove(EffectCommand effect)
        {
            lock (_lock)
            {
                _effects[effect.Handle].Remove(effect);

                _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] - 1;
            }
        }
        
        public bool CheckIfEffectApply(int effect)
        {
            return (int)_effectRoll[effect] > 0;
        }
    }
}