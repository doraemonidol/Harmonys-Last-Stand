using System;
using System.Collections.Generic;
using Cinemachine;
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
        [SerializeField] List<PWeapon> weapons;
        [SerializeField] private List<PlayerSkill> normalSkills;
        [SerializeField] private List<PlayerSkill> specialSkills;
        [SerializeField] private int _activeWeapon = 0;
        private int _currentSkill = -1;
        private double _beginChannelingTime = 0f;
        private RotateToMouseScript _rotateToMouse;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject target;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] public float timeScaleFactor = 0.3f;
        private EffectUIManager _effectUIManager;
        private Dictionary<Identity, float> skillNextAffectedTime = new Dictionary<Identity, float>();

        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(EntityType.AURELIA, this);
            // EquipWeapon(weapons[_activeWeapon]);
            Debug.Log("Aurelia: " + this.LogicHandle);
            
            _rotateToMouse = GetComponent<RotateToMouseScript>();
            if (!_rotateToMouse)
            {
                Debug.LogError("Please assign RotateToMouseScript to the player");
            }
            if (normalSkills.Count + specialSkills.Count != 4)
            {
                Debug.LogError("Please assign 4 skills to the player");
            }
        
            _rotateToMouse = GetComponent<RotateToMouseScript>();    
            if (!_rotateToMouse)
            {
                Debug.LogError("Please assign RotateToMouseScript to the player");
            }

            _effectUIManager = GetComponent<EffectUIManager>();
            if (!_effectUIManager)
            {
                Debug.LogError("Please assign EffectUIManager to the player");
            }
        
            UpdateCurrentSkills();
        }
        
        void UpdateCurrentSkills()
        {
            weapons[_activeWeapon].SetOwner(this.LogicHandle);
            
            for (int i = 0; i < normalSkills.Count; i++)
            {
                normalSkills[i] = (PlayerNormalSkill)weapons[_activeWeapon].GetNormalSkills()[i];
                normalSkills[i].UpdateChannelingTime(timeScaleFactor);
                normalSkills[i].AttachRotator(_rotateToMouse);
                normalSkills[i].AttachFirePoint(firePoint);
                normalSkills[i].AttachTarget(target);
                normalSkills[i].AttachVirtualCamera(virtualCamera);
            }
        
            for (int i = 0; i < specialSkills.Count; i++)
            {
                specialSkills[i] = (PlayerSpecialSkill)weapons[_activeWeapon].GetSpecialSkills()[i];
                specialSkills[i].AttachRotator(_rotateToMouse);
                specialSkills[i].AttachFirePoint(firePoint);
                specialSkills[i].AttachTarget(target);
                specialSkills[i].AttachVirtualCamera(virtualCamera);
            }
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
            ProcessSkillCasting();
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _activeWeapon = (_activeWeapon + 1) % weapons.Count;
                UpdateCurrentSkills();
            }
        }

        private void ProcessTranslation()
         {
             if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
             {
                 var currentDirection = new Vector3(0, 0, 0);
                 if (Input.GetAxisRaw("Horizontal") > 0)
                 {
                     currentDirection.x = 1;
                 }
                 else if (Input.GetAxisRaw("Horizontal") < 0)
                 {
                     currentDirection.x = -1;
                 }
                 if (Input.GetAxisRaw("Vertical") > 0)
                 {
                     currentDirection.z = 1;
                 }
                 else if (Input.GetAxisRaw("Vertical") < 0)
                 {
                     currentDirection.z = -1;
                 }

                 int direction =  currentDirection switch
                 {
                    {x: 1, z: 0} => ActionEvent.MoveRight,
                    {x: -1, z: 0} => ActionEvent.MoveLeft,
                    {x: 0, z: 1} => ActionEvent.MoveUp,
                    {x: 0, z: -1} => ActionEvent.MoveDown,
                    {x: 1, z: 1} => ActionEvent.MoveUpRight,
                    {x: 1, z: -1} => ActionEvent.MoveDownRight,
                    {x: -1, z: 1} => ActionEvent.MoveUpLeft,
                    {x: -1, z: -1} => ActionEvent.MoveDownLeft,
                    _ => throw new Exception("Invalid direction")
                };
                 // Debug.Log("Observed Move Event" + Time.time);
                 var eventd = new EventDto
                 {
                     Event = "MOVE",
                     ["identity"] = this.LogicHandle,
                     ["direction"] = direction
                 };
                 // Debug.Log("Move Event");
                 LogicLayer.GetInstance().Observe(eventd);
             }
         }
        
        void ProcessSkillCasting()
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.GetMouseButtonDown(i) && _currentSkill == -1)
                {
                    if (weapons[_activeWeapon].GetNormalSkills()[i].isReady)
                    {
                        var eventd = new EventDto
                        {
                            Event = "CAST",
                            ["activator"] = this.LogicHandle,
                            ["skill"] = weapons[_activeWeapon].GetNormalSkills()[i].LogicHandle
                        };
                        LogicLayer.GetInstance().Observe(eventd);
                        normalSkills[i].StartCasting();
                    }

                    if (weapons[_activeWeapon].GetSpecialSkills()[i].isReady)
                    {
                        _currentSkill = i;
                        _beginChannelingTime = Time.time;
                        normalSkills[i].StartChanneling();
                    }
                }

                if (_currentSkill != -1 && Time.time - _beginChannelingTime >= 0.1f)
                {
                    Channeling();
                }

                if (Input.GetMouseButtonUp(i) && _currentSkill == i)
                {
                    normalSkills[i].StopChanneling();
                    if (Time.time - _beginChannelingTime >= normalSkills[i].GetChannelingTime())
                    {
                        var eventd = new EventDto
                        {
                            Event = "CAST",
                            ["activator"] = this.LogicHandle,
                            ["skill"] = weapons[_activeWeapon].GetSpecialSkills()[i].LogicHandle
                        };
                        LogicLayer.GetInstance().Observe(eventd);
                        specialSkills[i].StartCasting();
                    }
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
                    int direction = (int)visitor["args"]["direction"];
                    float distance = (float)visitor["args"]["distance"];
                    Vector3 directionVector = direction switch
                    {
                        ActionEvent.MoveRight => new Vector3(1, 0, 0),
                        ActionEvent.MoveLeft => new Vector3(-1, 0, 0),
                        ActionEvent.MoveUp => new Vector3(0, 0, 1),
                        ActionEvent.MoveDown => new Vector3(0, 0, -1),
                        ActionEvent.MoveUpRight => new Vector3(1, 0, 1),
                        ActionEvent.MoveUpLeft => new Vector3(-1, 0, 1),
                        ActionEvent.MoveDownRight => new Vector3(1, 0, -1),
                        ActionEvent.MoveDownLeft => new Vector3(-1, 0, -1),
                        _ => throw new Exception("Invalid direction")
                    };
                    
                    distance *= 10;
                    
                    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                    {
                        // Debug.Log("Begin Movement");
                        transform.Translate(directionVector * (distance * Time.deltaTime), Space.World);
                    }
                    break;
                case "dead":
                    // Debug.Log("Aurelia Dead Animation");
                    break;
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
                    Debug.Log("Aurelia Shield Animation");
                    var duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.SHIELD,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.ROOTED:
                    // Debug.Log("Aurelia Rooted Animation");
                    break;
                case EffectType.CLONE:
                    // Debug.Log("Aurelia Clone Animation");
                    break;
                case EffectType.RESISTANCE:
                    duration = (int)visitor["args"]["timeout"];
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.RESISTANCE,
                        StartTime = Time.time,
                        Duration = duration
                    });
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
            Debug.Log("Aurelia Collided with " + other.gameObject.name);
            SkillColliderInfo info = other.gameObject.GetComponent<SkillColliderInfo>();
            if (info)
            {
                if ( !skillNextAffectedTime.ContainsKey(info.Skill) || Time.time > skillNextAffectedTime[info.Skill])
                {
                    Debug.Log("Aurelia Collided with " + other.gameObject.name);
                    // Debug.Log(other.GetComponent<SkillColliderInfo>().Attacker + " used " + 
                    //           other.GetComponent<SkillColliderInfo>().Skill + " on " + 
                    //           this.LogicHandle);
                    skillNextAffectedTime[info.Skill] = Time.time + info.affectCooldown;

                    var eventd = new EventDto
                    {
                        Event = "GET_ATTACKED",
                        ["attacker"] = info.Attacker,
                        ["target"] = this.LogicHandle,
                        ["context"] = null,
                        ["skill"] = info.Skill
                    };
                    LogicLayer.GetInstance().Observe(eventd);
                }
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            SkillColliderInfo info = other.GetComponent<SkillColliderInfo>();
            if (info)
            {
                if ( !skillNextAffectedTime.ContainsKey(info.Skill) || Time.time > skillNextAffectedTime[info.Skill])
                {
                    Debug.Log("Aurelia Particle Collided with " + other.name);
                    // Debug.Log(other.GetComponent<SkillColliderInfo>().Attacker + " used " + 
                    //           other.GetComponent<SkillColliderInfo>().Skill + " on " + 
                    //           this.LogicHandle);
                    skillNextAffectedTime[info.Skill] = Time.time + info.affectCooldown;

                    var eventd = new EventDto
                    {
                        Event = "GET_ATTACKED",
                        ["attacker"] = info.Attacker,
                        ["target"] = this.LogicHandle,
                        ["context"] = null,
                        ["skill"] = info.Skill
                    };
                    LogicLayer.GetInstance().Observe(eventd);
                }
            }
        }
    }
}