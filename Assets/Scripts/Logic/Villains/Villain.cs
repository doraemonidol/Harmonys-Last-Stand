using System;
using System.Collections.Generic;
using Common.Context;
using DTO;
using Logic.Effects;
using Logic.Helper;
using Logic.Villains.Maestro;
using Logic.Villains.States;
using Logic.Weapons;
using Object = UnityEngine.Object;

namespace Logic.Villains
{
    public abstract class Villain : LogicObject, ICharacter
    {
        public int Heath { get; set; }
        
        public int AtkSpeed { get; set; }
        
        public int MovSpeed { get; set; }
        
        public int Dmg { get; set; }
        
        private readonly EffectManager _effectManager;

        public Weapon VillainWeapon;

        public IVillainState State { get; set; }

        protected Villain()
        {
            _effectManager = new EffectManager();
        }

        protected Villain(LogicObject another) : base(another)
        {
            _effectManager = new EffectManager();
            State = new MmSkillCasting(this);
        }
        
        protected Villain(int hp, int atkSpeed, int movSpeed, int dmg) : this()
        {
            Heath = hp;
            this.AtkSpeed = atkSpeed;
            this.MovSpeed = movSpeed;
            this.Dmg = dmg;
        }
        
        public void SetState(IVillainState state)
        {
            State = state;
        }
        
        public void UpdateEffect(int ev, EventDto args = null)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual void CustomReceiveEffect(int ev, EventDto args = null)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveEffect(int ev, EventDto args = null)
        {
            try
            {
                switch (ev)
                {
                    case EffectHandle.Stunt:
                    {
                        OnVillainReceiveStunt(args);
                        break;
                    }
                    case EffectHandle.Bleeding:
                    {
                        OnVillainReceiveBleeding(args);
                        break;
                    }
                    case EffectHandle.KnockBack:
                    {
                        OnVillainReceiveKnockBack(args);
                        break;
                    }
                    case EffectHandle.Sleepy:
                    {
                        OnVillainReceiveSleepy(args);
                        break;
                    }
                    case EffectHandle.Resonance:
                    {
                        OnVillainReceiveResonance(args);
                        break;
                    }
                    case EffectHandle.Exhausted:
                    {
                        OnVillainReceiveExhausted(args);
                        break;
                    }
                    case EffectHandle.Rooted:
                    {
                        OnVillainReceiveRooted(args);
                        break;
                    }
                    case EffectHandle.GetHit:
                    {
                        if (args == null)
                        {
                            throw new ArgumentNullException(nameof(args));
                        }
                        OnVillainReceiveAtk(args);
                        break;
                    }
                    default:
                    {
                        CustomReceiveEffect(ev, args);
                        break;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }

        private void OnVillainReceiveRooted(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "rooted",
                }
            });
            _effectManager.Add(new Rooted(this, (int)args["timeout"]));
        }

        private void OnVillainReceiveExhausted(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "exhausted",
                },
                ["args"] =
                {
                    ["dmg"] = true,
                    ["movspd"] = false,
                    ["atkspd"] = false,
                    ["mana"] = false,
                    ["hp"] = false,
                }
            });
            _effectManager.Add(new Exhaust(this, (int)args["timeout"], new Dictionary<string, int>
            {
                ["hp"] = 0,
                ["dmg"] = 20,
                ["mana"] = 0,
                ["movspd"] = 0,
                ["atkspd"] = 0,
            }));
        }

        private void OnVillainReceiveAtk(EventDto args)
        {
            if (_effectManager.CheckIfEffectApply(EffectHandle.Sleepy))
            {
                _effectManager.Erase(EffectHandle.Sleepy);
            }

            var dmg = (int)args["dmg"];

            var cxt = GameContext.GetInstance();
            cxt.Do(BoostHandles.ReduceHealth, dmg);
            
            var currentHp = GameContext.GetInstance().Get("hp");
            
            if (IsDead(currentHp)) OnDead();
        }

        private static bool IsDead(int currentHp)
        {
            return currentHp <= 0;
        }

        private void OnVillainReceiveResonance(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "resonance",
                }
            });
            _effectManager.Add(new Resonance(this, (int)args["timeout"]));
        }

        private void OnVillainReceiveSleepy(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "sleepy",
                }
            });
            _effectManager.Add(new Sleepy(this, (int)args["timeout"]));
        }

        private void OnVillainReceiveKnockBack(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "knock-back",
                    ["distance"] = "2",
                }
            });
        }

        private void OnVillainReceiveBleeding(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "bleeding",
                }
            });
            _effectManager.Add(new Bleeding(this, (int)args["timeout"]));
        }

        private void OnVillainReceiveStunt(EventDto args)
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "stunt",
                }
            });
            _effectManager.Add(new Stunt(this, (int)args["timeout"]));
        }

        public bool IsEffectApplied(int ev)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnDead()
        {
            this.NotifySubscribers(new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = "dead",
                }
            });
        }

        public void Do(int action, Dictionary<string, Object> args)
        {
            switch (action)
            {
                case 0:
                {
                    State.OnStateUpdate(new Dictionary<string, object>
                    {
                        ["cast"] = true,
                    });
                    break;
                }
                case 1:
                {
                    State.OnStateUpdate(new Dictionary<string, object>
                    {
                        ["result"] = args["result"],
                    });
                    break;
                };
                default:
                {
                    break;
                }
            }
        }
        
        public List<int> GetAvailableSkills()
        {
            var result = new List<int>();
            var index = 0;
            foreach (var skill in VillainWeapon.Skills)
            {
                if (skill.IsAvailable())
                {
                    result.Add(index);
                }
                index++;
            }

            if (result.Count != 0) return result;
            var random = new Random().Next(0, 4);
            VillainWeapon.Skills[random].ImmediateUnlock();
            result.Add(random);
            
            return result;
        }
    }
}