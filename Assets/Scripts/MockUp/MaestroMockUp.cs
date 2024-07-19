using System;
using System.Collections.Generic;
using Common;
using DTO;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using Presentation.Bosses;
using Presentation.Maestro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Time = UnityEngine.Time;

namespace MockUp
{
    public class MaestroMockUp : BossMovement
    {

        private float _nextCastTime;

        [Header("Boss Stats")] [SerializeField]
        private WeaponMockUp _weapon;
        // private BossCastSkill _bossCastSkill;

        [SerializeField] private List<MaestroSkill> _skills;

        [Header("Skill Casting")] [SerializeField]
        private RotateToTargetScript _rotateToTarget;

        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject target;
        [SerializeField] private int testingSkill;

        private void InitializeSkills()
        {
            _weapon.SetOwner(this.LogicHandle);
            Debug.Log("Maestro: " + this.LogicHandle);
            Dictionary<string, Object> data = new Dictionary<string, Object>
            {
                ["animator"] = animator,
                ["target"] = target,
                ["navMeshAgent"] = navMeshAgent
            };
            
            for (int i = 0; i < _weapon.GetSkills().Count; i++)
            {
                _skills.Add((MaestroSkill)_weapon.GetSkills()[i]);
                _skills[i].Attach(data);
            }
        }

        public override void Start()
        {
            if (!_rotateToTarget)
            {
                Debug.LogError("Please assign RotateToTargetScript to the boss");
            }

            base.Start();
            // _bossCastSkill = GetComponent<BossCastSkill>();
            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "ama"), this);
            UpdateEnemyCollision();
            InitializeSkills();
        }

        public override void Update()
        {
            base.Update();

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                navMeshAgent.isStopped = true;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    animator.SetTrigger(EnemyActionType.Attack);
                return;
            }

            VillainCastSkill();

            // bool isCasting = false;
            // for (int i = 1; i <= _skills.Count; i++)
            // {
            //     if (animator.GetCurrentAnimatorStateInfo(0).IsName("CastSpell" + i.ToString()))
            //     {
            //         isCasting = true;
            //         break;
            //     }
            // }
            //
            // navMeshAgent.isStopped = isCasting;
        }

        private void VillainCastSkill()
        {
            if (Time.time < _nextCastTime) return;

            _nextCastTime = Time.time + Random.Range(3.0f, 10.0f);

            // var eventd = new EventDto
            // {
            //     Event = "VILLAIN_CAST",
            //     ["id"] = this.LogicHandle
            // };
            // LogicLayer.GetInstance().Observe(eventd);

            int skillIndex = Random.Range(0, _skills.Count);
            if (testingSkill != -1)
                skillIndex = testingSkill;

            if (CheckStartCast(skillIndex))
            {
                // Change z of gameObject.transform.rotation to -144
                gameObject.transform.localRotation = Quaternion.Euler(0, -144, 0);
                navMeshAgent.isStopped = true;
                switch (skillIndex)
                {
                    case 0:
                        animator.SetTrigger(EnemyActionType.CastSpell1);
                        break;
                    case 1:
                        animator.SetTrigger(EnemyActionType.CastSpell2);
                        break;
                    case 2:
                        animator.SetTrigger(EnemyActionType.CastSpell3);
                        break;
                    case 3:
                        animator.SetTrigger(EnemyActionType.CastSpell4);
                        break;
                    default:
                        Debug.LogError("Amadeus Unknown Cast: " + skillIndex);
                        break;
                }
            }
        }

        public bool CheckStartCast(int skillIndex)
        {
            // if (skillIndex < 0 || skillIndex >= skills.Count) return false;
            // if (!skills[skillIndex].IsOnCoolDown())
            // {
            // _skills[skillIndex].nextCastTime = Time.time + Random.Range(1.0f, 10.0f);
            if (_skills[skillIndex].IsReady())
            {
                StartCoroutine(_skills[skillIndex].StartCasting());
                return true;
            }
            else
            {
                return false;
            }
            // }
            //
            // return false;
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
    }
}