using System.Collections.Generic;
using Common;
using Logic.Context;
using Logic.Effects;
using Logic.Facade;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons;
using UnityEngine;

namespace Logic.MainCharacters
{
    public class Aurelia : LogicObject, IMainCharacter
    {
        private GameContext _context;
        
        private EffectManager _effectManager;

        private IWeapon _weapon;

        public void ReceiveEffect(int ev)
        {
            switch (ev)
            {
                case EffectHandle.Bleeding:
                    _context.Do();
                    var currentHp = _attributes.Query("g", "hp");
                    if (IsDead(currentHp)) OnDead();
                    break;
            }
        }

        public void Do(int action, Dictionary<string, Object> args)
        {
            var logicWorld = LogicWorld.GetInstance();
            switch (action)
            {
                case Action.MoveLeft:
                case Action.MoveRight:
                case Action.MoveUp:
                case Action.MoveDown:
                    
            }
            throw new System.NotImplementedException();
            
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