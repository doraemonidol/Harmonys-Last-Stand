using System.Collections;
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
    public class Troop : BossMovement
    {
        public float eyeSight = 80f;
        public WeaponMockUp weapon;
        
        public override void Start()
        {
            base.Start();
            // LogicLayer.GetInstance().Instantiate(Google.Search("ins", "troop"), this);
            UpdateEnemyCollision();
            
            for (int i = 0; i < enemyCollisions.Count; i++)
            {
                SkillColliderInfo skillColliderInfo = enemyCollisions[i].gameObject.AddComponent<SkillColliderInfo>();
                skillColliderInfo.Attacker = this.LogicHandle;
                skillColliderInfo.Skill = weapon.GetSkills()[0].LogicHandle;
            }
        }

        public override void Update()
        {
            if (isDead)
                return;
            
            if (Vector3.Distance(transform.position, player.transform.position) <= eyeSight)
            {
                if (!isAppeared)
                {
                    animator.SetTrigger(EnemyActionType.Appear);
                    isAppeared = true;
                }
                
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    navMeshAgent.isStopped = true;
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        animator.SetTrigger(EnemyActionType.Attack);
                }
                else
                {
                    base.Update();
                    navMeshAgent.isStopped = false;
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
                        animator.SetTrigger(EnemyActionType.Move);
                }
            }
            else
            {
                navMeshAgent.ResetPath();
            }
        }
        
        public override IEnumerator OnDeadAnimation()
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }

        // public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        // {
        //     switch (visitor["ev"]["type"])
        //     {
        //         case "dead":
        //             // switch (visitor["args"]["strategy"])
        //             // {
        //             //     case "duplicate":
        //             //         Debug.Log("Troop Duplicate Animation");
        //             //         break;
        //             //     default:
        //             //         Debug.Log("Troop Default Animation");
        //             //         break;
        //             // }
        //             break;
        //         case "start-effect":
        //             switch (visitor["args"]["name"])
        //             {
        //                 case EffectType.STUNT:
        //                     Debug.Log("Troop Stunt Animation");
        //                     break;
        //                 case EffectType.BLEEDING:
        //                     Debug.Log("Troop Bleeding Animation");
        //                     break;
        //                 case EffectType.KNOCKBACK:
        //                     Debug.Log("Troop Knockback Animation");
        //                     break;
        //                 case EffectType.SLEEPY:
        //                     Debug.Log("Troop Sleepy Animation");
        //                     break;
        //                 case EffectType.RESONANCE:
        //                     Debug.Log("Troop Resonance Animation");
        //                     break;
        //                 case EffectType.ROOTED:
        //                     Debug.Log("Troop Rooted Animation");
        //                     break;
        //                 case EffectType.EXHAUSTED:
        //                     Debug.Log("Troop Exhausted Animation");
        //                     break;
        //                 case EffectType.GET_HIT:
        //                     Debug.Log("Troop Get Hit Animation");
        //                     break;
        //                 default:
        //                     Debug.Log("Troop Default Animation");
        //                     break;
        //             }
        //             break;
        //         case "end-effect":
        //             switch (visitor["args"]["name"])
        //             {
        //                 case EffectType.STUNT:
        //                     Debug.Log("Troop Stunt Animation End");
        //                     break;
        //                 case EffectType.BLEEDING:
        //                     Debug.Log("Troop Bleeding Animation End");
        //                     break;
        //                 case EffectType.KNOCKBACK:
        //                     Debug.Log("Troop Knockback Animation End");
        //                     break;
        //                 case EffectType.SLEEPY:
        //                     Debug.Log("Troop Sleepy Animation End");
        //                     break;
        //                 case EffectType.RESONANCE:
        //                     Debug.Log("Troop Resonance Animation End");
        //                     break;
        //                 case EffectType.ROOTED:
        //                     Debug.Log("Troop Rooted Animation End");
        //                     break;
        //                 case EffectType.EXHAUSTED:
        //                     Debug.Log("Troop Exhausted Animation End");
        //                     break;
        //                 case EffectType.GET_HIT:
        //                     Debug.Log("Troop Get Hit Animation End");
        //                     break;
        //                 default:
        //                     Debug.Log("Troop Default Animation End");
        //                     break;
        //             }
        //             break;
        //         default:
        //             Debug.LogError("Unknown Event: " + visitor["ev"]["type"]);
        //             break;
        //     }
        // }
    }
}