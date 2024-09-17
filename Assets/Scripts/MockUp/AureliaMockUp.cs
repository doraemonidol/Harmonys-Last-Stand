using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using DTO;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using Presentation.GUI;
using Presentation.Manager;
using Presentation.UI;
using UnityEngine;
using Time = UnityEngine.Time;

namespace MockUp
{
    public class AureliaMockUp : PresentationObject
    {
        private List<SkillDetailUI> skillDetailUIs;
        [SerializeField] List<PWeapon> weapons;
        [SerializeField] public List<PlayerSkill> normalSkills;
        [SerializeField] public List<PlayerSkill> specialSkills;
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
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Transform body;
        private Animator _animator;
        private bool isStopped = false;
        
        private bool isDead = false;
        public CloneCastSkill cloneCastSkill;

        [SerializeField] private float dashingPower = 50f;
        [SerializeField] private float dashDuration = 0.12f;
        private float dashCooldown = 1f;
        private bool isDashing = false;
        private bool canDash = true;
        private bool toDash = false;
        private int oldDirection = 0;
        private SkillDetailUI dashSkillDetailUI;

        private void SetHealth(int health, int maxHealth)
        {
            healthBar.currentHealth = health;
            healthBar.maxHealth = maxHealth;
        }
        
        public int GetActiveWeapon()
        {
            return _activeWeapon;
        }

        public override void Start()
        {
            if (_activeWeapon != -1)
            {
                InitializeWeapon();
            }
            isDead = false;
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
            
            _animator = body.GetComponent<Animator>();
            if (!_animator)
            {
                Debug.LogError("Please assign Animator to the player");
            }
            
            // Find all gameobjects with script SkillDetailUI
            skillDetailUIs = new List<SkillDetailUI>();
            foreach (var skillDetailUI in FindObjectsOfType<SkillDetailUI>())
            {
                skillDetailUIs.Add(skillDetailUI);
            }
            
            // sort the skillDetailUIs by their name
            skillDetailUIs.Sort((a, b) => a.name.CompareTo(b.name));
            
            dashSkillDetailUI = skillDetailUIs[^1];
        
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
                normalSkills[i].AttachSkillDetailUI(skillDetailUIs[i * 2]);
                skillDetailUIs[i * 2].SetIcon(normalSkills[i].GetIcon());
            }
        
            for (int i = 0; i < specialSkills.Count; i++)
            {
                specialSkills[i] = (PlayerSpecialSkill)weapons[_activeWeapon].GetSpecialSkills()[i];
                specialSkills[i].AttachRotator(_rotateToMouse);
                specialSkills[i].AttachFirePoint(firePoint);
                specialSkills[i].AttachTarget(target);
                specialSkills[i].AttachVirtualCamera(virtualCamera);
                specialSkills[i].AttachSkillDetailUI(skillDetailUIs[i * 2 + 1]);
                skillDetailUIs[i * 2 + 1].SetIcon(specialSkills[i].GetIcon());
            }
            
            if (cloneCastSkill)
            {
                cloneCastSkill.UpdateCurrentSkills();
            }
        }

        private void ChangeWeapon()
        {
            var weapon1 = DataManager.Instance.ToggleWeapon();
            _activeWeapon = FindWeaponIndex(weapon1);
        }

        private void InitializeWeapon()
        {
            var weapon = DataManager.Instance.LoadData().Weapon1;
            _activeWeapon = FindWeaponIndex(weapon);
        }
        
        private int FindWeaponIndex(EntityTypeEnum weapon)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].entityType == weapon)
                {
                    return i;
                }
            }
            return -1;
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
            if (isDead || GameManager.Instance.IsGamePaused)
                return;
            
            ProcessTranslation();
            
            if (_activeWeapon != -1)
            {
                ProcessSkillCasting();

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ChangeWeapon();
                    UpdateCurrentSkills();
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
            }
        }

        private void ProcessTranslation()
         {
             if (isStopped)
             {
                 return;
             }
             
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
                    {x: 1, z: 0} => ActionEvent.MoveDownRight,
                    {x: -1, z: 0} => ActionEvent.MoveUpLeft,
                    {x: 0, z: 1} => ActionEvent.MoveUpRight,
                    {x: 0, z: -1} => ActionEvent.MoveDownLeft,
                    {x: 1, z: 1} => ActionEvent.MoveRight,
                    {x: -1, z: -1} => ActionEvent.MoveLeft,
                    {x: 1, z: -1} => ActionEvent.MoveDown,
                    {x: -1, z: 1} => ActionEvent.MoveUp,
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
             else
             {
                 _animator.SetBool("isRunning", false);
             }
             
             if (Input.GetKeyDown(KeyCode.Space) && canDash)
             {
                 Debug.Log("Dash");
                 toDash = true;
                 var eventd = new EventDto
                 {
                     Event = "MOVE",
                     ["identity"] = this.LogicHandle,
                     ["direction"] = oldDirection
                 };
                 // Debug.Log("Move Event");
                 LogicLayer.GetInstance().Observe(eventd);
             }
         }

        private void OnCasting()
        {
            isStopped = true;
            body.rotation = Quaternion.LookRotation(_rotateToMouse.GetDirection());
            StartCoroutine(OnCastEnd());
        }
        
        private IEnumerator OnCastEnd()
        {
            yield return new WaitForSeconds(0.1f);
            isStopped = false;
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
                        if (cloneCastSkill)
                        {
                            cloneCastSkill.CastNormalSkill(i);
                        }
                        OnCasting();
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
                        if (cloneCastSkill)
                        {
                            cloneCastSkill.CastSpecialSkill(i);
                        }
                        OnCasting();
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
                    
                    if (toDash)
                    {
                        toDash = false;
                        Debug.Log("oldDirection: " + oldDirection);
                        Debug.Log("directionVector: " + directionVector);
                        StartCoroutine(Dash(directionVector));
                    } else if (!isDashing)
                    {
                        distance *= 10;
                        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                        {
                            oldDirection = direction;
                            // Debug.Log("Begin Movement");
                            _animator.SetBool("isRunning", true);
                            transform.Translate(directionVector * (distance * Time.deltaTime), Space.World);
                            body.rotation = Quaternion.LookRotation(directionVector);
                        }
                    }
                    break;
                case "dead":
                    Debug.Log("Aurelia Dead Animation");
                    GameManager.Instance.GameOver();
                    break;
                case EffectType.STUNT:
                    // Debug.Log("Aurelia Stunt Animation");
                    var duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.STUNT,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.BLEEDING:
                    // Debug.Log("Aurelia Bleeding Animation");
                    
                    if (visitor["args"]["timeout"] == null)
                    {
                        var hpDrain = (int)visitor["args"][EffectHandle.HpDrain];
                        
                    }
                    else
                    {
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.BLEEDING,
                            StartTime = Time.time,
                            Duration = duration
                        });
                    }
                    break;
                case EffectType.HALLUCINATION:
                    // Debug.Log("Aurelia Hallucination Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.HALLUCINATION,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.FEAR:
                    // Debug.Log("Aurelia Fear Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.FEAR,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.NEARSIGHT:
                    // Debug.Log("Aurelia Nearsight Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.NEARSIGHT,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.SHIELD:
                    Debug.Log("Aurelia Shield Animation");
                    duration = (int)visitor["args"]["timeout"];
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
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.ROOTED,
                        StartTime = Time.time,
                        Duration = duration
                    });
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
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.JINX,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.KNOCKBACK:
                    // Debug.Log("Aurelia Knockback Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.KNOCKBACK,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.REVERSE:
                    // Debug.Log("Aurelia Reverse Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.REVERSE,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.CHARM:
                    // Debug.Log("Aurelia Charm Animation");
                    duration = (int)visitor["args"]["timeout"];
                    Debug.Log(duration);
                    _effectUIManager.AddEffect(new EffectUI()
                    {
                        Name = EffectType.CHARM,
                        StartTime = Time.time,
                        Duration = duration
                    });
                    break;
                case EffectType.GET_HIT:
                    var currentHealth = (int)visitor["args"]["current-health"];
                    var maxHealth = (int)visitor["args"]["max-health"];
                    var damage = (int)visitor["args"]["damage"];
                    Debug.Log("Aurelia Get Hit " + damage + " damage" + " Current Health: " + currentHealth + " Max Health: " + maxHealth);
                    SetHealth(currentHealth, maxHealth);
                    
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
                            Debug.Log("Aurelia Rooted Animation End");
                            _effectUIManager.RemoveEffect(EffectType.ROOTED);
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
                            Debug.LogError("Aurelia Unknown Cast: " + visitor["args"]["skill-index"]);
                            break;
                    }
                    break;
                default:
                    Debug.LogError("Unknown Event: " + visitor["ev"]["type"]);
                    break;
            }
        }

        private IEnumerator Dash(Vector3 direction)
        {
            dashSkillDetailUI.Initialize(dashCooldown);
            isDashing = true;
            canDash = false;
            float time = 0;
            while (time < dashDuration)
            {
                transform.Translate(direction * (dashingPower * Time.deltaTime), Space.World);
                time += Time.deltaTime;
                yield return null;
            }
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            // Debug.Log("Aurelia Collided with " + other.gameObject.name);
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