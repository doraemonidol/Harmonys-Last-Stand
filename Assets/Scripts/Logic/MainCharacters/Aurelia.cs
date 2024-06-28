using System;
using System.Collections.Generic;
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
        
        private EffectManager _effectManager;

        private readonly List<IWeapon> _weapons;

        private IWeapon _activeWeapon;

        public Aurelia()
        {
            _weapons = new List<IWeapon>();
        }

        public Aurelia(List<int> wpLists)
        {
            _weapons = new List<IWeapon>();
            foreach (var weapon in wpLists)
            {
                _weapons.Add(IWeapon.TransformInto(weapon));
            }
            _activeWeapon = _weapons[0];
        }
        
        public void ReceiveEffect(int ev, EventDto args = null)
        {
            try
            {
                var timeout = (int)args![EffectHandle.Timeout];
                switch (ev)
                {
                    case EffectHandle.Bleeding:
                        var hpDrain = (int)args![EffectHandle.HpDrain];
                        _effectManager.Add(new Bleeding(this, timeout,
                            new Dictionary<string, int>
                            {
                                [EffectHandle.HpDrain] = hpDrain
                            }));
                        break;
                    case EffectHandle.Stunt:
                        _effectManager.Add(new Stunt(this, timeout));
                        break;
                    case EffectHandle.Hallucinate:
                        // _effectManager.Add(new Hallucination(this, timeout));
                        break;
                    case EffectHandle.Fear:
                        // _effectManager.Add(new Fear(this, timeout));
                        break;
                    case EffectHandle.Nearsight:
                        
                        break;
                    case EffectHandle.Shielded:
                        break;
                    case EffectHandle.Rooted:
                        break;
                    case EffectHandle.SlowDown:
                        break;
                    case EffectHandle.Silent:
                        break;
                    case EffectHandle.Healing:
                        _context.Do(BoostHandles.BoostHealth, (int)args![EffectHandle.HpGain]);
                        break;
                    case EffectHandle.GetHit:
                        _context.Do(BoostHandles.ReduceHealth, (int)args![EffectHandle.HpReduce]);
                        var currentHp = _context.Get("hp");
                        if (IsDead(currentHp)) OnDead();
                        break;
                    default:
                        throw new Exception("Thrown at Aurelia.ReceiveEffect(). Invalid effect type.");
                }
            } catch (NullReferenceException e) {
                Debug.Log("Thrown at Aurelia.ReceiveEffect(). Arguments is null.\n" + e.Message);
                throw;
            }
        }

        public bool IsEffectApplied(int ef)
        {
            return _effectManager.CheckIfEffectApply(ef);
        }

        private void UpdateEffectOnBleeding(EventDto args = null)
        {
            _context.Do(BoostHandles.ReduceHealth, (int)args![EffectHandle.HpDrain]);
            var currentHp = _context.Get("hp");
            if (IsDead(currentHp)) OnDead();
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
                        break;
                    case EffectHandle.Hallucinate:
                        break;
                    case EffectHandle.Fear:
                        break;
                    case EffectHandle.Nearsight:
                        break;
                    case EffectHandle.Shielded:
                        break;
                    case EffectHandle.Rooted:
                        break;
                    case EffectHandle.SlowDown:
                        break;
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
                        var currentHp = _context.Get("hp");
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
                case Action.MOVE_LEFT:
                case Action.MOVE_RIGHT:
                case Action.MOVE_UP:
                case Action.MOVE_DOWN:
                case Action.MOVE_UP_RIGHT:
                case Action.MOVE_UP_LEFT:
                case Action.MOVE_DOWN_RIGHT:
                case Action.MOVE_DOWN_LEFT:
                {
                    if (_effectManager.CheckIfEffectApply(EffectHandle.Hallucinate))
                    {
                        throw new Exception("Thrown at Aurelia.Do(). Cannot move while hallucinating.");
                    }
                    break;
                }
            }
            throw new System.NotImplementedException();
            
        }

        public void ReceiveNewWeapon(int weapon)
        {
            _weapons.Add(IWeapon.TransformInto(weapon));
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
                ["ev"] =
                {
                    ["target"] = this
                },
                ["ev"] =
                {
                    ["source"] = this
                }
            };
            NotifySubscribers(visitor: visitor);
        }
    }
}