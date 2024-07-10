using System.Collections.Generic;
using Common;
using DTO;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using Presentation.Bosses;
using UnityEngine;
using Time = UnityEngine.Time;

namespace MockUp
{
    public class AmadeusMockUp : PresentationObject
    {
        private float lastCastTime = 0.0f;
        private float cooldownTime = 1.0f;
        public WeaponMockUp weapon;
        
        [SerializeField] private List<BossSkill> skills;
        [SerializeField] private RotateToTargetScript _rotateToTarget;
        
        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject target;
        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "ama"), this);
            if (!_rotateToTarget)
            {
                Debug.LogError("Please assign RotateToTargetScript to the boss");
            }
            
            InitializeSkills();
        }

        private void InitializeSkills()
        {
            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].AttachRotator(_rotateToTarget);
                skills[i].AttachFirePoint(firePoint);
                skills[i].AttachTarget(target);
            }
            
        }

        public override void Update()
        {
            VillainCastSkill();
        }

        private void VillainCastSkill()
        {
            if (Time.time - lastCastTime < cooldownTime) return;
            
            lastCastTime = Time.time;
            cooldownTime = Random.Range(1.0f, 5.0f);
            
            var eventd = new EventDto
            {
                Event = "VILLAIN_CAST",
                ["id"] = this.LogicHandle
            };
            LogicLayer.GetInstance().Observe(eventd);
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            switch (visitor["ev"]["type"])
            {
                case "dead":
                    Debug.Log("Amadeus Dead Animation");
                    break;
                case "start-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Amadeus Stunt Animation");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Amadeus Bleeding Animation");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Amadeus Knockback Animation");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Amadeus Sleepy Animation");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Amadeus Resonance Animation");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Amadeus Rooted Animation");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Amadeus Exhausted Animation");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Amadeus Get Hit Animation");
                            break;
                        default:
                            Debug.Log("Amadeus Default Animation");
                            break;
                    }
                    break;
                case "end-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Amadeus Stunt Animation End");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Amadeus Bleeding Animation End");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Amadeus Knockback Animation End");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Amadeus Sleepy Animation End");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Amadeus Resonance Animation End");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Amadeus Rooted Animation End");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Amadeus Exhausted Animation End");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Amadeus Get Hit Animation End");
                            break;
                        default:
                            Debug.Log("Amadeus Default Animation End");
                            break;
                    }
                    break;
                case "cast":
                    switch (visitor["args"]["skill-index"])
                    {
                        case 0:
                            Debug.Log("Amadeus Cast Skill 0 Animation");
                            break;
                        case 1:
                            Debug.Log("Amadeus Cast Skill 1 Animation");
                            break;
                        case 2:
                            Debug.Log("Amadeus Cast Skill 2 Animation");
                            break;
                        case 3:
                            Debug.Log("Amadeus Cast Skill 3 Animation");
                            break;
                        case 4:
                            Debug.Log("Amadeus Cast Skill 4 Animation");
                            break;
                        default:
                            Debug.LogError("Amadeus Unknown Cast: " + visitor["args"]["skill-index"]);
                            break;
                    }
                    break;
                default:
                    Debug.LogError("Unknown Event: " + visitor["ev"]["type"]);
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