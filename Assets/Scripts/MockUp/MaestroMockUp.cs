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
    public class MaestroMockUp : PresentationObject
    {
        private float lastCastTime = 0.0f;
        private float cooldownTime = 1.0f;
        public WeaponMockUp weapon;
        
        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "mae"), this);
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
                    Debug.Log("Maestro Dead Animation");
                    break;
                case "start-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Maestro Stunt Animation");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Maestro Bleeding Animation");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Maestro Knockback Animation");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Maestro Sleepy Animation");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Maestro Resonance Animation");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Maestro Rooted Animation");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Maestro Exhausted Animation");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Maestro Get Hit Animation");
                            break;
                        case EffectType.RESURRECTION:
                            Debug.Log("Maestro Resurrection Animation");
                            break;
                        default:
                            Debug.Log("Maestro Default Animation");
                            break;
                    }
                    break;
                case "end-effect":
                    switch (visitor["args"]["name"])
                    {
                        case EffectType.STUNT:
                            Debug.Log("Maestro Stunt Animation End");
                            break;
                        case EffectType.BLEEDING:
                            Debug.Log("Maestro Bleeding Animation End");
                            break;
                        case EffectType.KNOCKBACK:
                            Debug.Log("Maestro Knockback Animation End");
                            break;
                        case EffectType.SLEEPY:
                            Debug.Log("Maestro Sleepy Animation End");
                            break;
                        case EffectType.RESONANCE:
                            Debug.Log("Maestro Resonance Animation End");
                            break;
                        case EffectType.ROOTED:
                            Debug.Log("Maestro Rooted Animation End");
                            break;
                        case EffectType.EXHAUSTED:
                            Debug.Log("Maestro Exhausted Animation End");
                            break;
                        case EffectType.GET_HIT:
                            Debug.Log("Maestro Get Hit Animation End");
                            break;
                        case EffectType.RESURRECTION:
                            Debug.Log("Maestro Resurrection Animation End");
                            break;
                        default:
                            Debug.Log("Maestro Default Animation End");
                            break;
                    }
                    break;
                case "cast":
                    switch (visitor["args"]["skill-index"])
                    {
                        case 0:
                            Debug.Log("Maestro Cast Skill 0 Animation");
                            break;
                        case 1:
                            Debug.Log("Maestro Cast Skill 1 Animation");
                            break;
                        case 2:
                            Debug.Log("Maestro Cast Skill 2 Animation");
                            break;
                        case 3:
                            Debug.Log("Maestro Cast Skill 3 Animation");
                            break;
                        case 4:
                            Debug.Log("Maestro Cast Skill 4 Animation");
                            break;
                        default:
                            Debug.LogError("Maestro Unknown Cast: " + visitor["args"]["skill-index"]);
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