using System.Collections.Generic;
using Common;
using DTO;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;
using Presentation.Bosses;
using Presentation.GUI;
using UnityEngine;
using Time = UnityEngine.Time;

namespace MockUp
{
    public class AmadeusMockUp : BossMovement
    {
        private float _nextCastTime;
        [Header("Boss Stats")]
        [SerializeField] private WeaponMockUp _weapon;
        // private BossCastSkill _bossCastSkill;
        
        [SerializeField] private List<BossSkill> _skills;
        [Header("Skill Casting")]
        [SerializeField] private RotateToTargetScript _rotateToTarget;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject target;
        [SerializeField] private int testingSkill;

        private void InitializeSkills()
        {
            _weapon.SetOwner(this.LogicHandle);
            Debug.Log("Amadeus: " + this.LogicHandle);
            for (int i = 0; i < _weapon.GetSkills().Count; i++)
            {
                _skills.Add((BossSkill)_weapon.GetSkills()[i]);
                _skills[i].AttachRotator(_rotateToTarget);
                _skills[i].AttachFirePoint(firePoint);
                _skills[i].AttachTarget(target);
            }
        }

        public override void OnDead()
        {
            base.OnDead();
            GameManager.Instance.OnDefeatBoss(SceneTypeEnum.AMADEUS_BOSS);
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
            if (isDead || GameManager.Instance.IsGamePaused)
                return;
            
            base.Update();
            
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                navMeshAgent.isStopped = true;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    animator.SetTrigger(EnemyActionType.Attack);
                return;
            }
            
            VillainCastSkill();

            bool isCasting = false;
            for (int i = 1; i <= _skills.Count; i++)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("CastSpell" + i.ToString()))
                {
                    isCasting = true;
                    break;
                }
            }

            navMeshAgent.isStopped = isCasting;
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
            
            if (StartCasting(skillIndex))
            {
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
        
        public bool StartCasting(int skillIndex)
        {
            // if (skillIndex < 0 || skillIndex >= skills.Count) return false;
            // if (!skills[skillIndex].IsOnCoolDown())
            // {
            _skills[skillIndex].nextCastTime = Time.time + Random.Range(1.0f, 10.0f);
            _skills[skillIndex].StartCasting();
            return true;
            // }
            //
            // return false;
        }
    }
}