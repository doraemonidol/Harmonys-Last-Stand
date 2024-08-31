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
    public class LudwigMockUp : PresentationObject
    {
        private float lastCastTime = 0.0f;
        private float cooldownTime = 1.0f;
        public WeaponMockUp weapon;
        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "lud"), this);
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
                    Debug.Log("Ludwig Dead Animation");
                    break;
                case "start-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Ludwig Stunt Animation");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Ludwig Bleeding Animation");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Ludwig Knockback Animation");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Ludwig Sleepy Animation");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Ludwig Resonance Animation");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Ludwig Rooted Animation");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Ludwig Exhausted Animation");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Ludwig Get Hit Animation");
                            break;
                        default:
                            Debug.Log("Ludwig Default Animation");
                            break;
                    }
                    break;
                case "end-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Ludwig Stunt Animation End");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Ludwig Bleeding Animation End");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Ludwig Knockback Animation End");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Ludwig Sleepy Animation End");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Ludwig Resonance Animation End");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Ludwig Rooted Animation End");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Ludwig Exhausted Animation End");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Ludwig Get Hit Animation End");
                            break;
                        default:
                            Debug.Log("Ludwig Default Animation End");
                            break;
                    }
                    break;
                case "cast":
                    switch (visitor["args"]["skill-index"])
                    {
                        case 0:
                            Debug.Log("Ludwig Cast Skill 0 Animation");
                            break;
                        case 1:
                            Debug.Log("Ludwig Cast Skill 1 Animation");
                            break;
                        case 2:
                            Debug.Log("Ludwig Cast Skill 2 Animation");
                            break;
                        case 3:
                            Debug.Log("Ludwig Cast Skill 3 Animation");
                            break;
                        case 4:
                            Debug.Log("Ludwig Cast Skill 4 Animation");
                            break;
                        default:
                            Debug.LogError("Ludwig Unknown Cast: " + visitor["args"]["skill-index"]);
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