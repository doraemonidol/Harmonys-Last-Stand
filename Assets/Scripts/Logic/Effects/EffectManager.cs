using System;
using System.Collections;
using System.Collections.Generic;
using Common.Context;
using Logic.Helper;
using Logic.MainCharacters;

namespace Logic.Effects
{
    public class EffectManager
    {


        private readonly Dictionary<int, ArrayList> _effects = new();
        
        private readonly object _lock = new object();

        private LogicObject _user;
        
        // Assume that there are 32 effects in total.
        // private int _effectRoll = 0;

        private ArrayList _effectRoll = new ArrayList(32);

        public EffectManager()
        {
            for (var i = 0; i < 32; i++)
            {
                _effects.Add(i, new ArrayList());
                _effectRoll.Add(0);
            }
        }

        public EffectManager(LogicObject user) : this() {
            _user = user;
        }

        public static string IntoEventString(int ev)
        {
            return ev switch
            {
                EffectHandle.DisableHealing => "disable_healing",
                EffectHandle.DisableHallucinate => "disable_hallucination",
                EffectHandle.DisableExhausted => "disable_exhausted",
                EffectHandle.DisableBleeding => "disable_bleeding",
                EffectHandle.DisableCharm => "disable_charm",
                EffectHandle.DisableClone => "disable_clone",
                EffectHandle.DisableFear => "disable_fear",
                EffectHandle.DisableJinxed => "disable_jinxed",
                EffectHandle.DisableNearsight => "disable_nearsight",
                EffectHandle.DisableRooted => "disable_rooted",
                EffectHandle.DisableSilent => "disable_silent",
                EffectHandle.DisableStunt => "disable_stunt",
                EffectHandle.DisableShielded => "disable_shielded",
                _ => throw new Exception("Thrown at Aurelia.IntoEventString(). Invalid effect type." + ev)
            };
        }
        
        public void Refresh(int effectHandle)
        {
            lock (_lock)
            {
                var cloneEffects = (ArrayList)_effects[effectHandle].Clone();
                
                foreach (var effect in cloneEffects)
                {
                    if (((EffectCommand)effect).IsExpired())
                    {
                        Remove((EffectCommand)effect);
                    }
                }

                if (_effects[effectHandle].Count == 0)
                {
                    _effectRoll[effectHandle] = 0;

                    _user.NotifySubscribers(new EventUpdateVisitor {
                        ["ev"] = {
                            ["type"] = IntoEventString(effectHandle),
                        }
                    });
                }
            }
        }
        
        public void Add(EffectCommand effect)
        {
            lock (_lock)
            {
                _effects[effect.Handle].Add(effect);
                effect.GetManagedBy(this);
                
                if (effect.Handle == EffectHandle.Jinxed)
                {
                    UpdateStatsWhenJinxedStart(effect);
                }
                else if (effect.Handle == EffectHandle.Exhausted)
                {
                    UpdateStatsWhenExhaustStart(effect);
                }
                effect.Execute();
                _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] + 1;
            }
        }

        private static void UpdateStatsWhenExhaustStart(EffectCommand effect)
        {
            var range = ((Exhaust)effect).ExRange;
            if ((range & 16) != 0)
            {
                var dmg = ((Exhaust)effect).ExDmg;
                var cxt = GameContext.GetInstance();
                var chDmg = cxt.Get("dmg+");
                var nextChDmg = (100 + chDmg) * (100 - dmg) / 100;
                cxt.Set("dmg+", nextChDmg - 100);
            }
            if ((range & 4) != 0)
            {
                var atkSpd = ((Exhaust)effect).ExASp;
                var cxt = GameContext.GetInstance();
                var chAtkSpd = cxt.Get("atkSpd+");
                var nextChAtkSpd = (100 + chAtkSpd) * (100 - atkSpd) / 100;
                cxt.Set("atkSpd+", nextChAtkSpd - 100);
            }
            if ((range & 2) != 0)
            {
                var movSpd = ((Exhaust)effect).ExMSp;
                var cxt = GameContext.GetInstance();
                var chMovSpd = cxt.Get("movSpd+");
                var nextChMovSpd = (100 + chMovSpd) * (100 - movSpd) / 100;
                cxt.Set("movSpd+", nextChMovSpd - 100);
            }
        }

        private static void UpdateStatsWhenJinxedStart(EffectCommand effect)
        {
            var range = ((Jinxed)effect).BoostRange;
            if ((range & 16) != 0)
            {
                var dmg = ((Jinxed)effect).BoostDmg;
                var cxt = GameContext.GetInstance();
                var chDmg = cxt.Get("dmg+");
                var nextChDmg = (100 + chDmg) * (100 + dmg) / 100;
                cxt.Set("dmg+", nextChDmg - 100);
            }

            if ((range & 4) != 0)
            {
                var atkSpd = ((Jinxed)effect).BoostAtkSpd;
                var cxt = GameContext.GetInstance();
                var chAtkSpd = cxt.Get("atkSpd+");
                var nextChAtkSpd = (100 + chAtkSpd) * (100 + atkSpd) / 100;
                cxt.Set("atkSpd+", nextChAtkSpd - 100);
            }
                    
            if ((range & 2) != 0)
            {
                var movSpd = ((Jinxed)effect).BoostMovSpd;
                var cxt = GameContext.GetInstance();
                var chMovSpd = cxt.Get("movSpd+");
                var nextChMovSpd = (100 + chMovSpd) * (100 + movSpd) / 100;
                cxt.Set("movSpd+", nextChMovSpd - 100);
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

        private void Remove(EffectCommand effect)
        {
            lock (_lock)
            {
                effect.StopGetManaged();

                if (effect.Handle == EffectHandle.Jinxed)
                {
                    UpdateStatsWhenJinxedEnd(effect);
                }
                else if (effect.Handle == EffectHandle.Exhausted)
                {
                    UpdateStatsWhenExhaustEnd(effect);
                }
                
                _effects[effect.Handle].Remove(effect);

                _effectRoll[effect.Handle] = (int)_effectRoll[effect.Handle] - 1;
            }
        }

        private void UpdateStatsWhenExhaustEnd(EffectCommand effect)
        {
            var range = ((Exhaust)effect).ExRange;
            if ((range & 16) != 0)
            {
                var finalDmg = 100;
                for (var i = 0; i < _effects[EffectHandle.Exhausted].Count; i++)
                {
                    var e = (Exhaust)_effects[EffectHandle.Exhausted][i];
                    if (e == effect) continue;
                    if ((e.ExRange & 16) != 0)
                    {
                        finalDmg = finalDmg * (100 - e.ExDmg) / 100;
                    }
                }
                GameContext.GetInstance().Set("dmg+", finalDmg - 100);
            }
            if ((range & 4) != 0)
            {
                var finalAtkSpd = 100;
                for (var i = 0; i < _effects[EffectHandle.Exhausted].Count; i++)
                {
                    var e = (Exhaust)_effects[EffectHandle.Exhausted][i];
                    if (e == effect) continue;
                    if ((e.ExRange & 4) != 0)
                    {
                        finalAtkSpd = finalAtkSpd * (100 - e.ExASp) / 100;
                    }
                }
                GameContext.GetInstance().Set("atkSpd+", finalAtkSpd - 100);
            }
            if ((range & 2) != 0)
            {
                var finalMovSpd = 100;
                for (var i = 0; i < _effects[EffectHandle.Exhausted].Count; i++)
                {
                    var e = (Exhaust)_effects[EffectHandle.Exhausted][i];
                    if (e == effect) continue;
                    if ((e.ExRange & 2) != 0)
                    {
                        finalMovSpd = finalMovSpd * (100 - e.ExMSp) / 100;
                    }
                }
                GameContext.GetInstance().Set("movSpd+", finalMovSpd - 100);
            }
        }

        private void UpdateStatsWhenJinxedEnd(EffectCommand effect)
        {
            var range = ((Jinxed)effect).BoostRange;
            if ((range & 16) != 0)
            {
                var finalDmg = 100;
                for (var i = 0; i < _effects[EffectHandle.Jinxed].Count; i++)
                {
                    var e = (Jinxed)_effects[EffectHandle.Jinxed][i];
                    if (e == effect) continue;
                    if ((e.BoostRange & 16) != 0)
                    {
                        finalDmg = finalDmg * (100 + e.BoostDmg) / 100;
                    }
                }
                GameContext.GetInstance().Set("dmg+", finalDmg - 100);
            }
            if ((range & 4) != 0)
            {
                var finalAtkSpd = 100;
                for (var i = 0; i < _effects[EffectHandle.Jinxed].Count; i++)
                {
                    var e = (Jinxed)_effects[EffectHandle.Jinxed][i];
                    if (e == effect) continue;
                    if ((e.BoostRange & 4) != 0)
                    {
                        finalAtkSpd = finalAtkSpd * (100 + e.BoostAtkSpd) / 100;
                    }
                }
                GameContext.GetInstance().Set("atkSpd+", finalAtkSpd - 100);
            }
            if ((range & 2) != 0)
            {
                var finalMovSpd = 100;
                for (var i = 0; i < _effects[EffectHandle.Jinxed].Count; i++)
                {
                    var e = (Jinxed)_effects[EffectHandle.Jinxed][i];
                    if (e == effect) continue;
                    if ((e.BoostRange & 2) != 0)
                    {
                        finalMovSpd = finalMovSpd * (100 + e.BoostMovSpd) / 100;
                    }
                }
                GameContext.GetInstance().Set("movSpd+", finalMovSpd - 100);
            }
        }

        public bool CheckIfEffectApply(int effect)
        {
            return (int)_effectRoll[effect] > 0;
        }
    }
}