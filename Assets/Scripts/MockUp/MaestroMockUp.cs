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

        private float _nextCastTime = 0;

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
                ["navMeshAgent"] = navMeshAgent,
                ["firePoint"] = firePoint,
            };
            
            for (int i = 0; i < _weapon.GetSkills().Count; i++)
            {
                _skills.Add((MaestroSkill)_weapon.GetSkills()[i]);
                _skills[i].Attach(data);
                _skills[i].MaestroLogicHandle = this.LogicHandle;
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
            // LogicLayer.GetInstance().Instantiate(Google.Search("ins", "mae"), this);
            UpdateEnemyCollision();
            InitializeSkills();
        }

        public override void Update()
        {
            if (isDead)
                return;

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

            // var eventd = new EventDto
            // {
            //     Event = "VILLAIN_CAST",
            //     ["id"] = this.LogicHandle
            // };
            // LogicLayer.GetInstance().Observe(eventd);

            int skillIndex = Random.Range(0, _skills.Count - 1);
            if (testingSkill != -1)
                skillIndex = testingSkill;

            Debug.Log("Check casting skill " + skillIndex);
            if (CheckStartCast(skillIndex))
            {

                _nextCastTime = Time.time + Random.Range(5.0f, 10.0f) + _skills[skillIndex].GetTimeout();
                Debug.Log("Start casting skill " + skillIndex);
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
                        // animator.SetTrigger(EnemyActionType.CastSpell3);
                        break;
                    case 3:
                        animator.SetTrigger(EnemyActionType.CastSpell4);
                        break;
                    default:
                        Debug.LogError("Maestro Unknown Cast: " + skillIndex);
                        break;
                }
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.transform.position);
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
    }
}