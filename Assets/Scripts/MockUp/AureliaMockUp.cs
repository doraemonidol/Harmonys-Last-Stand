using System;
using System.Collections.Generic;
using Common;
using DTO;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using UnityEngine;
using Time = UnityEngine.Time;

namespace MockUp
{
    public class AureliaMockUp : PresentationObject
    {
        List<PWeapon> weapons;
        private int _activeWeapon = 0;
        private int _currentSkill = -1;
        private double _beginChannelingTime = 0f;
        [SerializeField] public float timeScaleFactor = 0.3f;

        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(EntityType.AURELIA, this);
        }

        public void ChangeWeapon(PWeapon weapon)
        {
            var eventd = new EventDto
            {
                Event = "CHANGE_WP",
            };
            LogicLayer.GetInstance().Observe(eventd);
            
            weapons.Remove(weapon);
        }

        public void EquipWeapon(PWeapon weapon)
        {
            var eventd = new EventDto
            {
                Event = "TAKE_WP",
                ["char"] = this.LogicHandle,
                ["num"] = 1,
                ["wp1"] = weapon.LogicHandle
            };
            LogicLayer.GetInstance().Observe(eventd);
            
            weapons.Add(weapon);
        }

        public override void Update()
        {
            ProcessTranslation();
        }

        private void ProcessTranslation()
         {
             if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
             {
                 var currentDirection = new Vector3(0, 0, 0);
                 if (Input.GetAxis("Horizontal") > 0)
                 {
                     currentDirection.x = 1;
                 }
                 else if (Input.GetAxis("Horizontal") < 0)
                 {
                     currentDirection.x = -1;
                 }
                 if (Input.GetAxis("Vertical") > 0)
                 {
                     currentDirection.z = 1;
                 }
                 else if (Input.GetAxis("Vertical") < 0)
                 {
                     currentDirection.z = -1;
                 }

                 var eventd = new EventDto
                 {
                     Event = "MOVE",
                     ["char"] = this.LogicHandle,
                     ["direction"] = currentDirection
                 };
                LogicLayer.GetInstance().Observe(eventd);
             }
         }
        
        void ProcessSkillCasting()
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.GetMouseButtonDown(i) && _currentSkill == -1)
                {
                    var eventd = new EventDto
                    {
                        Event = "CAST",
                        ["skill"] = weapons[_activeWeapon].GetNormalSkills()[i].LogicHandle
                    };
                    LogicLayer.GetInstance().Observe(eventd);
                }

                if (_currentSkill != -1 && Time.time - _beginChannelingTime >= 0.1f)
                {
                    Channeling();
                }

                if (Input.GetMouseButtonUp(i) && _currentSkill == i)
                {
                    var eventd = new EventDto
                    {
                        Event = "CAST",
                        ["skill"] = weapons[_activeWeapon].GetSpecialSkills()[i].LogicHandle
                    };
                    LogicLayer.GetInstance().Observe(eventd);
                    // normalSkills[i].StopChanneling();
                    // if (Time.time - _beginChannelingTime >= normalSkills[i].channelingTime)
                    // {
                    //     Debug.Log("Cast Skill " + i);
                    //     specialSkills[i].StartCasting();
                    // }
                    _currentSkill = -1;
                }
            }
        }

        private void Channeling()
        {
            Time.timeScale = timeScaleFactor;
        }
        
        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            switch (visitor["ev"]["type"])
            {
                case "move":
                    // Vector2 direction = (Vector2)visitor["args"]["direction"];
                    // float distance = (float)visitor["args"]["distance"];
                    //
                    // transform.position = new Vector3(
                    //     transform.position.x + direction.x * distance,
                    //     transform.position.y,
                    //     transform.position.z + direction.y * distance
                    // );
                    // break;
                case "dead":
                    // Debug.Log("Aurelia Dead Animation");
                    break;
                case "start-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            // Debug.Log("Aurelia Stunt Animation");
                            break;
                        case EffectType.BLEEDING:
                            // Debug.Log("Aurelia Bleeding Animation");
                            break;
                        case EffectType.HALLUCINATION:
                            // Debug.Log("Aurelia Hallucination Animation");
                            break;
                        case EffectType.FEAR:
                            // Debug.Log("Aurelia Fear Animation");
                            break;
                        case EffectType.NEARSIGHT:
                            // Debug.Log("Aurelia Nearsight Animation");
                            break;
                        case EffectType.SHIELD:
                            // Debug.Log("Aurelia Shield Animation");
                            break;
                        case EffectType.ROOTED:
                            // Debug.Log("Aurelia Rooted Animation");
                            break;
                        case EffectType.CLONE:
                            // Debug.Log("Aurelia Clone Animation");
                            break;
                        case EffectType.RESISTANCE:
                            // Debug.Log("Aurelia Resistance Animation");
                            break;
                        case EffectType.JINX:
                            // Debug.Log("Aurelia Jinx Animation");
                            break;
                        case EffectType.KNOCKBACK:
                            // Debug.Log("Aurelia Knockback Animation");
                            break;
                        case EffectType.REVERSE:
                            // Debug.Log("Aurelia Reverse Animation");
                            break;
                        case EffectType.CHARM:
                            // Debug.Log("Aurelia Charm Animation");
                            break;
                        case EffectType.GET_HIT:
                            // Debug.Log("Aurelia Get Hit Animation");
                            break;
                        default:
                            // Debug.Log("Aurelia Default Animation");
                            break;
                    }

                    break;
                case "end-effect":
                    switch (visitor["args"]["name"])
                    {
                        case "stunt":
                            // Debug.Log("Aurelia Stunt Animation End");
                            break;
                        case EffectType.BLEEDING:
                            // Debug.Log("Aurelia Bleeding Animation End");
                            break;
                        case EffectType.HALLUCINATION:
                            // Debug.Log("Aurelia Hallucination Animation End");
                            break;
                        case EffectType.FEAR:
                            // Debug.Log("Aurelia Fear Animation End");
                            break;
                        case EffectType.NEARSIGHT:
                            // Debug.Log("Aurelia Nearsight Animation End");
                            break;
                        case EffectType.SHIELD:
                            // Debug.Log("Aurelia Shield Animation End");
                            break;
                        case EffectType.ROOTED:
                            // Debug.Log("Aurelia Rooted Animation End");
                            break;
                        case EffectType.CLONE:
                            // Debug.Log("Aurelia Clone Animation End");
                            break;
                        case EffectType.RESISTANCE:
                            // Debug.Log("Aurelia Resistance Animation End");
                            break;
                        case EffectType.JINX:
                            // Debug.Log("Aurelia Jinx Animation End");
                            break;
                        case EffectType.KNOCKBACK :
                            // Debug.Log("Aurelia Knockback Animation End");
                            break;
                        case EffectType.REVERSE:
                            // Debug.Log("Aurelia Reverse Animation End");
                            break;
                        case EffectType.CHARM:
                            // Debug.Log("Aurelia Charm Animation End");
                            break;
                        case EffectType.GET_HIT:
                            // Debug.Log("Aurelia Get Hit Animation End");
                            break;
                        default:
                            // Debug.Log("Aurelia Default Animation End");
                            break;
                    }
                    break;
                case "change-wp":
                    // Debug.Log("Aurelia Change Weapon Animation");
                    break;
                case "cast":
                    switch (visitor["args"]["skill-index"])
                    {
                        case 0:
                            // Debug.Log("Aurelia Cast Skill 0 Animation");
                            break;
                        case 1:
                            // Debug.Log("Aurelia Cast Skill 1 Animation");
                            break;
                        case 2:
                            // Debug.Log("Aurelia Cast Skill 2 Animation");
                            break;
                        case 3:
                            // Debug.Log("Aurelia Cast Skill 3 Animation");
                            break;
                        case 4:
                            // Debug.Log("Aurelia Cast Skill 4 Animation");
                            break;
                        default:
                            // Debug.LogError("Aurelia Unknown Cast: " + visitor["args"]["skill-index"]);
                            break;
                    }
                    break;
                default:
                    // Debug.LogError("Unknown Event: " + visitor["ev"]["type"]);
                    break;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<SkillColliderInfo>() == null) return;
            
            var eventd = new EventDto
            {
                Event = "GET_ATTACKED",
                ["attacker"] = other.gameObject.GetComponent<SkillColliderInfo>().Attacker,
                ["target"] = this.LogicHandle,
                ["context"] = null,
                ["skill"] = other.gameObject.GetComponent<SkillColliderInfo>().Skill
            };
            LogicLayer.GetInstance().Observe(eventd);
        }
    }
}