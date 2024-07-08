using System;
using System.Collections.Generic;
using Common;
using Common.Context;
using DTO;
using Logic.Context;
using Logic.Effects;
using Logic.Helper;
using Logic.Weapons;
using UnityEngine;
using Action = Common.ActionEvent;
using Object = UnityEngine.Object;

namespace Logic.MainCharacters
{
    public class Aurelia : LogicObject, IMainCharacter
    {
        private GameContext _context;
        
        private readonly EffectManager _effectManager;

        public List<Weapon> _weapons { get; }

        private Weapon _activeWeapon;

        public Aurelia()
        {
            _weapons = new List<Weapon>();
        }

        public Aurelia(List<int> wpLists)
        {
            _effectManager = new EffectManager();
            _weapons = new List<Weapon>();
            foreach (var weapon in wpLists)
            {
                _weapons.Add(Weapon.TransformInto(weapon));
            }
            _activeWeapon = _weapons[0];
        }

        public void TakeWeapon(List<Weapon> wpLists)
        {
            foreach (var weapon in wpLists)
            {
                _weapons.Add(weapon);
            }
            _activeWeapon = _weapons[0];
        }
        
        /// <summary>
        /// This represents the idea of receiving effect from other characters.
        /// </summary>
        /// <param name="ev">The event</param>
        /// <param name="args">Dictionary of arguments</param>
        /// <exception cref="NullReferenceException">Throw when argument is missing.</exception>
        /// 
        public void ReceiveEffect(int ev, EventDto args = null)
        {
            try
            {
                var timeout = (int)args![EffectHandle.Timeout];
                switch (ev)
                {
                    case EffectHandle.Bleeding:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        var hpDrain = (int)args![EffectHandle.HpDrain];
                        _effectManager.Add(new Bleeding(this, timeout,
                            new Dictionary<string, int>
                            {
                                [EffectHandle.HpDrain] = hpDrain
                            }));
                        break;
                    }
                    case EffectHandle.Stunt:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        _effectManager.Add(new Stunt(this, timeout));
                        break;
                    }
                    case EffectHandle.Hallucinate:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }
                        // _effectManager.Add(new Hallucination(this, timeout));
                        break;
                    }
                    case EffectHandle.Fear:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        // _effectManager.Add(new Fear(this, timeout));
                        break;
                    }
                    case EffectHandle.Nearsight:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        break;
                    }
                    case EffectHandle.Shielded:
                    {
                        break;
                    }
                    case EffectHandle.Rooted:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        break;
                    }
                    case EffectHandle.SlowDown:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        break;
                    }
                    case EffectHandle.Silent:
                    {
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Resistance))
                        {
                            return;
                        }

                        break;
                    }
                    case EffectHandle.Clone:
                    {
                        break;
                    }
                    case EffectHandle.Resistance:
                    {
                        break;
                    }
                    case EffectHandle.Charm:
                    {
                        break;
                    }
                    case EffectHandle.Healing:
                    {
                        _context.Do(BoostHandles.BoostHealth, (int)args![EffectHandle.HpGain]);
                        break;
                    }
                    case EffectHandle.GetHit:
                    {
                        var dmg = (float) args![EffectHandle.HpReduce];
                        if (_effectManager.CheckIfEffectApply(EffectHandle.Shielded))
                        {
                            dmg *= 0.3f;
                        }
                        _context.Do(BoostHandles.ReduceHealth, (int) dmg);
                        var currentHp = GameContext.GetInstance().Get("hp");
                        if (IsDead(currentHp)) OnDead();
                        break;
                    }
                    case EffectHandle.DisableStunt:
                    case EffectHandle.DisableHallucinate:
                    case EffectHandle.DisableBleeding:
                    case EffectHandle.DisableNearsight:
                    case EffectHandle.DisableRooted:
                    case EffectHandle.DisableFear:
                    case EffectHandle.DisableCharm:
                    case EffectHandle.DisableClone:
                    case EffectHandle.DisableHealing:
                    case EffectHandle.DisableSilent:
                        this.NotifySubscribers(new EventUpdateVisitor
                        {
                            ["ev"] =
                            {
                                ["type"] = IntoEventString(ev),
                            }
                        });
                        break;
                    case EffectHandle.DisableExhausted:
                    case EffectHandle.DisableJinxed:
                        _context.Do(BoostHandles.Reset);
                        break;
                    default:
                        throw new Exception("Thrown at Aurelia.ReceiveEffect(). Invalid effect type.");
                }
            } catch (NullReferenceException e) {
                Debug.Log("Thrown at Aurelia.ReceiveEffect(). Arguments is null.\n" + e.Message);
                throw;
            }
        }

        private static string IntoEventString(int ev)
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
                _ => throw new Exception("Thrown at Aurelia.IntoEventString(). Invalid effect type.")
            };
        }

        public bool IsEffectApplied(int ef)
        {
            return _effectManager.CheckIfEffectApply(ef);
        }

        public void UpdateEffect(int ev, EventDto args = null)
        {
            try
            {
                switch (ev)
                {
                    case EffectHandle.Bleeding:
                    {
                        UpdateEffectOnBleeding(args);
                        break;
                    }
                    case EffectHandle.Stunt:
                    case EffectHandle.Hallucinate:
                    case EffectHandle.Fear:
                    case EffectHandle.Nearsight:
                    case EffectHandle.Shielded:
                    case EffectHandle.Rooted:
                    case EffectHandle.SlowDown:
                    case EffectHandle.Silent:
                        break;
                    case EffectHandle.Healing:
                    {
                        _context.Do(BoostHandles.BoostHealth, (int)args![EffectHandle.HpGain]);
                        break;
                    }
                    case EffectHandle.GetHit:
                    {
                        _context.Do(BoostHandles.ReduceHealth, (int)args![EffectHandle.HpReduce]);
                        var currentHp = GameContext.GetInstance().Get("hp");
                        if (IsDead(currentHp)) OnDead();
                        break;
                    }
                    default:
                        throw new Exception("Thrown at Aurelia.ReceiveEffect(). Invalid effect type.");
                }
            } catch (System.NullReferenceException e) {
                Debug.Log(e.Message);
                throw;
            }
        }

        public void Do(int action, Dictionary<string, Object> args)
        {
            switch (action)
            {
                case Action.MoveLeft:
                case Action.MoveRight:
                case Action.MoveUp:
                case Action.MoveDown:
                case Action.MoveUpLeft:
                case Action.MoveUpRight:
                case Action.MoveDownLeft:
                case Action.MoveDownRight:
                {
                    var logicAction = action;
                    
                    if (_effectManager.CheckIfEffectApply(EffectHandle.Stunt)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Fear)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Charm)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Rooted)
                    ) {
                        return;
                    }
                    
                    if (_effectManager.CheckIfEffectApply(EffectHandle.Hallucinate))
                    {
                        logicAction = Action.FlipAction(action);
                    }
                    
                    this.NotifySubscribers(new EventUpdateVisitor
                    {
                        ["ev"] =
                        {
                            ["type"] = "move",
                            ["direction"] = logicAction,
                            ["distance"] = GameContext.GetInstance().Get("moveSpeed"),
                        }
                    });
                    break;
                }
                case Action.CastSkill1:
                case Action.CastSkill2:
                case Action.CastSkill3:
                case Action.CastSkill4:
                {
                    if (_effectManager.CheckIfEffectApply(EffectHandle.Stunt)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Fear)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Charm)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Sleepy)
                        || _effectManager.CheckIfEffectApply(EffectHandle.Silent)
                    ) {
                        return;
                    }
                    this._activeWeapon.Skills[action - 8].Activate();
                    break;
                }
            }
            throw new System.NotImplementedException();
        }

        public void ReceiveNewWeapon(int weapon)
        {
            _weapons.Add(Weapon.TransformInto(weapon));
        }

        public void Switch(int wpIndex)
        {
            _activeWeapon = _weapons[wpIndex];
        }

        public bool IsDead(int health)
        {
            return health <= 0;
        }

        public void OnDead()
        {
            var visitor = new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "dead"
                },
            };
            NotifySubscribers(visitor: visitor);
        }

        private void UpdateEffectOnBleeding(EventDto args = null)
        {
            _context.Do(BoostHandles.ReduceHealth, (int)args![EffectHandle.HpDrain]);
            var currentHp = GameContext.GetInstance().Get("hp");
            if (IsDead(currentHp)) OnDead();
        }
    }
}