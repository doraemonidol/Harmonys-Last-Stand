using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Skills;
using Logic.Villains;
using Logic.Villains.Amadeus;
using Logic.Villains.Ludwig;
using Logic.Villains.Maestro;
using Logic.Villains.States;
using Logic.Weapons;
using Presentation;
using UnityEditor;

namespace Logic.Facade
{
    public class LogicLayer : ILogicFacade
    {
        private static LogicLayer _instance;
        
        private static readonly object Lock = new object();

        public Dictionary<Identity, object> _objects { get; }

        public Dictionary<object, Identity> _identities { get; }
        
        public static void Register(object obj, Identity identity)
        {
            _instance._objects.Add(identity, obj);
            _instance._identities.Add(obj, identity);
        }

        private LogicLayer()
        {
            _objects = new Dictionary<Identity, object>();
            _identities = new Dictionary<object, Identity>();
        }

        private static LogicObject Create(int type)
        {
            return type switch
            {
                EntityType.AURELIA => new Aurelia(),
                EntityType.FLUTE 
                    or EntityType.PIANO 
                    or EntityType.SUPERBASS 
                    or EntityType.GUITAR 
                    or EntityType.SAXOPHONE 
                    or EntityType.VIOLON 
                    or EntityType.WEAPON_MAESTRO
                    or EntityType.WEAPON_LUDWIG
                    or EntityType.WEAPON_AMADEUS
                        => Weapon.TransformInto(type),
                EntityType.AMADEUS => new AmadeusPrime(),
                EntityType.LUDWIG => new LudwigVanVortex(),
                EntityType.MAESTRO => new MaestroMachina(),
                _ => null
            };
        }

        public static LogicLayer GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            lock (Lock)
            {
                return _instance ??= new LogicLayer();
            }
        }
        
        /**
         * <param name="type">The type of the object/ See also: <seealso cref="EntityType"/></param>
         * <param name="presRef">The presentation object itself.</param>
         * <summary>
         * Instantiate an object in the logic view.
         * Subscribe the presentation object to the logic object.
         * Finally, return the identity of the logic object.
         * </summary>
         */
        public void Instantiate(int type, PresentationObject presRef)
        {
            switch (type)
            {
                case EntityType.AURELIA:
                case EntityType.AMADEUS:
                case EntityType.LUDWIG:
                case EntityType.MAESTRO:
                {
                    var identity = new Identity();
                    var logicRef = Create(type);

                    Register(logicRef, identity);

                    presRef.Subscribe(logicRef);
                    logicRef.Subscribe(presRef);

                    presRef.SelfHandle = new Identity();
                    presRef.LogicHandle = identity;

                    logicRef.SelfHandle = identity;
                    logicRef.PresentationHandle = presRef.SelfHandle;
                    break;
                }
                case EntityType.VIOLON:
                case EntityType.FLUTE:
                case EntityType.GUITAR:
                case EntityType.SAXOPHONE:
                case EntityType.PIANO:
                case EntityType.SUPERBASS:
                case EntityType.WEAPON_LUDWIG:
                case EntityType.WEAPON_MAESTRO:
                case EntityType.WEAPON_AMADEUS:
                {
                    var identity = new Identity();

                    var logicRef = (Weapon) Create(type);

                    presRef.Subscribe(logicRef);
                    logicRef.Subscribe(presRef);

                    foreach (var i in Enumerable.Range(0, ((PWeapon)presRef).GetSkills().Count))
                    {
                        var presSkill = ((PWeapon)presRef).GetSkills()[i];
                        var logicSkill = logicRef.Skills[i];

                        presSkill.Subscribe(logicSkill);
                        logicSkill.Subscribe(presSkill);

                        presSkill.SelfHandle = new Identity();
                        var logicSkillIdentity = new Identity();
                        logicSkill.SelfHandle = logicSkillIdentity;

                        presSkill.LogicHandle = logicSkill.SelfHandle;
                        logicSkill.PresentationHandle = presSkill.SelfHandle;

                        Register(logicSkill, logicSkillIdentity);
                    }

                    presRef.SelfHandle = new Identity();
                    logicRef.SelfHandle = identity;
                        
                    Register(logicRef, identity);
                        
                    presRef.LogicHandle = logicRef.SelfHandle;
                    logicRef.PresentationHandle = presRef.SelfHandle;
                        
                    break;
                }
                default:
                {
                    throw new Exception("Unknown type.");
                }
            }
        }
        
        /**
         * <param name="identity">The identity of the object to be destroyed.</param>
         * <summary>
         * Destroy the Logic View of the object.
         * </summary>
         */
        public void Destroy(Identity identity)
        {
            var obj = _objects[identity];
            _objects.Remove(identity);
            _identities.Remove(obj);
        }
        
        /**
         * <param name="eventDto">The event to be handled.</param>
         * <summary>
         * Handle the event.
         * </summary>
         */
        public void Observe(EventDto eventDto)
        {
            var ev = eventDto.Event;
            var identity = eventDto["identity"] as Identity;
            Console.WriteLine(ev);
            switch (ev)
            {
                case "MOVE":
                {
                    if (identity != null)
                    {
                        var character = (IMainCharacter)_objects[identity];
                        var direction = eventDto["direction"] as int? ?? -1;
                        character.Do(
                            direction,
                            null
                        );
                    }
                    break;
                }
                case "TAKE_WP":
                {
                    var character = (Aurelia)_objects[(Identity)eventDto["char"]];
                    var number = eventDto["num"] as int? ?? -1;
                    if (number < 0)
                    {
                        throw new Exception("Invalid number of weapon. You need to declare the number of weapons to take.");
                    }
                    var weapons = Enumerable.Range(0, number).Select(i => (Weapon)_objects[(Identity)eventDto[$"wp{i + 1}"]]).ToList();
                    character.TakeWeapon(weapons);
                    break;
                }
                case "CHANGE_WP":
                    break;
                case "VILLAIN_CAST":
                {
                    var villain = (Villain)_objects[(Identity)eventDto["id"]];
                    if (villain.State.GetType() == typeof(MmSkillCasting) 
                        || villain.State.GetType() == typeof(LwSkillCasting)
                        || villain.State.GetType() == typeof(AmadeusSkillCasting)
                    ) {
                        villain.State.OnStateUpdate(new Dictionary<string, object>
                        {
                            ["cast"] = true,
                        });
                    }

                    break;
                }
                case "VILLAIN_RESULT":
                {
                    var villain = (Villain)_objects[(Identity)eventDto["id"]];
                    if (villain.State.GetType() == typeof(ResultWaiting))
                    {
                        villain.State.OnStateUpdate(new Dictionary<string, object>
                        {
                            ["result"] = eventDto["result"]
                        });
                    }
                    break;
                }
                case "CAST":
                {
                    var activator = (ICharacter) _objects[(Identity) eventDto["activator"]];
                    var skill = (AcSkill) _objects[(Identity) eventDto["skill"]];
                    activator.Do(ActionEvent.CastSkill, new Dictionary<string, object>
                    {
                        ["skill"] = skill,
                    });
                    break;
                }
                case "GET_ATTACKED":
                {
                    var attacker = (ICharacter) _objects[(Identity) eventDto["attacker"]];
                    var victim = (ICharacter) _objects[(Identity) eventDto["target"]];
                    var context = eventDto["context"] as EventDto;
                    var skill = (AcSkill) _objects[(Identity) eventDto["skill"]];
                    skill.Affect(attacker, victim, context);
                    break;
                }
                case "BUY_OUT_GAME":
                    var item = eventDto["item"] as int? ?? -1;
                    switch (item)
                    {
                        case Item.HEALTH_POTION:
                        {
                            break;
                        }
                        case Item.MANA_POTION:
                        {
                            break;
                        }
                        case Item.ATK_SPEED_POTION:
                        {
                            break;
                        }
                        case Item.MOVE_SPEED_POTION:
                        {
                            break;
                        }
                        case Item.DAMAGE_POTION:
                        {
                            break;
                        }
                        case Item.PIANO:
                        case Item.FLUTE:
                        case Item.SUPER_BASS:
                        case Item.GUITAR:
                        case Item.SAXOPHONE:
                        case Item.VIOLIN:
                            break;
                    }
                    break;
                case "BUY_IN_GAME":
                {
                    var _item = eventDto["item"] as int? ?? -1;
                    switch (_item)
                    {
                        case Item.HEALTH_POTION:
                        {
                            break;
                        }
                        case Item.MANA_POTION:
                        {
                            break;
                        }
                        case Item.ATK_SPEED_POTION:
                        {
                            break;
                        }
                        case Item.MOVE_SPEED_POTION:
                        {
                            break;
                        }
                        case Item.DAMAGE_POTION:
                        {
                            break;
                        }
                    }
                    break;
                }
                default:
                    throw new Exception("Unknown event.");
            }
        }
    }
}