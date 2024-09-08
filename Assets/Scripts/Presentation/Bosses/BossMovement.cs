using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Logic;
using Logic.Facade;
using Presentation;
using Presentation.Bosses;
using Presentation.GUI;
using Presentation.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[Serializable]
public abstract class BossMovement : PresentationObject
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject player;
    protected bool isDead;
    protected bool isAppeared = false;

    protected Animator animator;
    [Header("Enemy Base")]
    [SerializeField] protected float attackRange;

    [SerializeField] protected HealthBar healthBar;
    protected EffectUIManager _effectUIManager;
    
    [SerializeField] protected List<EnemyCollision> enemyCollisions;

    public override void Start()
    {
        LogicLayer.GetInstance().Instantiate(EntityType.GetEntityType(entityType), this);
        Debug.Log("Entity: " + this.LogicHandle + " " + entityType);
        isDead = false;
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        // animator.SetTrigger(EnemyActionType.Move);

        _effectUIManager = GetComponent<EffectUIManager>();
        if (!_effectUIManager)
        {
            Debug.LogError("Please assign EffectUIManager to the boss");
        }
        UpdateEnemyCollision();
    }
    
    protected void SetHealth(int currentHealth, int maxHealth)
    {
        healthBar.currentHealth = currentHealth;
        healthBar.maxHealth = maxHealth;
    }

    public void UpdateEnemyCollision()
    {
        foreach (var enemyCollision in enemyCollisions)
        {
            Debug.Log("Updating Enemy Collision");
            enemyCollision.LogicHandle = this.LogicHandle;
            enemyCollision.Handle = LogicHandle.Handle;
        }
    }

    public virtual void OnDead()
    {
        isDead = true;
        navMeshAgent.isStopped = true;
        animator.SetTrigger(EnemyActionType.Die);
        StartCoroutine(OnDeadAnimation());
    }
    
    public virtual IEnumerator OnDeadAnimation()
    {
        yield return new WaitForSeconds(0);
    }
    
    public IEnumerator OnResurrection(int currentHealth, int maxHealth)
    {
        animator.SetTrigger(EnemyActionType.CastSpell4);
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        navMeshAgent.isStopped = false;
        yield return new WaitForSeconds(1f);
        SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isDead || GameManager.Instance.IsGamePaused)
            return;
        navMeshAgent.SetDestination(player.transform.position);
    }

    public override void AcceptAndUpdate(EventUpdateVisitor visitor)
    {
        if (isDead)
        {
            return;
        }
        
        switch (visitor["ev"]["type"])
        {
            case "dead":
                Debug.Log("Boss Dead Animation");
                OnDead();
                break;
            case "start-effect":
                switch (visitor["args"]["name"])
                {
                    case EffectType.STUNT:
                        Debug.Log("Boss Stunt Animation");
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
                        Debug.Log("Boss Bleeding Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.BLEEDING,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.KNOCKBACK:
                        Debug.Log("Boss Knockback Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.KNOCKBACK,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.SLEEPY:
                        Debug.Log("Boss Sleepy Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.SLEEPY,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.RESONANCE:
                        Debug.Log("Boss Resonance Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.RESONANCE,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.ROOTED:
                        Debug.Log("Boss Rooted Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.ROOTED,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.EXHAUSTED:
                        Debug.Log("Boss Exhausted Animation");
                        duration = (int)visitor["args"]["timeout"];
                        Debug.Log(duration);
                        _effectUIManager.AddEffect(new EffectUI()
                        {
                            Name = EffectType.EXHAUSTED,
                            StartTime = Time.time,
                            Duration = duration
                        });
                        break;
                    case EffectType.RESURRECTION:
                        Debug.Log("Boss Resurrection Animation");
                        var currentHealth = (int)visitor["args"]["current-health"];
                        var maxHealth = (int)visitor["args"]["max-health"];
                        
                        StartCoroutine(OnResurrection(currentHealth, maxHealth));
                        break;
                    case EffectType.GET_HIT:
                        Debug.Log("Boss Get Hit Animation");
                        currentHealth = (int)visitor["args"]["current-health"];
                        maxHealth = (int)visitor["args"]["max-health"];
                        var damage = (int)visitor["args"]["damage"];
                        
                        SetHealth(currentHealth, maxHealth);
                        break;
                    default:
                        Debug.LogError("Boss Unknown Event " + visitor["args"]["name"]);
                        break;
                }
                break;
            case "end-effect":
                switch (visitor["args"]["name"])
                {
                    case EffectType.STUNT:
                        Debug.Log("Boss Stunt Animation End");
                        break;
                    case EffectType.BLEEDING:
                        Debug.Log("Boss Bleeding Animation End");
                        break;
                    case EffectType.KNOCKBACK:
                        Debug.Log("Boss Knockback Animation End");
                        break;
                    case EffectType.SLEEPY:
                        Debug.Log("Boss Sleepy Animation End");
                        break;
                    case EffectType.RESONANCE:
                        Debug.Log("Boss Resonance Animation End");
                        break;
                    case EffectType.ROOTED:
                        Debug.Log("Boss Rooted Animation End");
                        break;
                    case EffectType.EXHAUSTED:
                        Debug.Log("Boss Exhausted Animation End");
                        break;
                    case EffectType.GET_HIT:
                        Debug.Log("Boss Get Hit Animation End");
                        break;
                    default:
                        Debug.Log("Boss Default Animation End");
                        break;
                }
                break;
            case "cast":
                switch (visitor["args"]["skill-index"])
                {
                    case 0:
                        Debug.Log("Boss Cast Skill 0 Animation");
                        break;
                    case 1:
                        Debug.Log("Boss Cast Skill 1 Animation");
                        break;
                    case 2:
                        Debug.Log("Boss Cast Skill 2 Animation");
                        break;
                    case 3:
                        Debug.Log("Boss Cast Skill 3 Animation");
                        break;
                    case 4:
                        Debug.Log("Boss Cast Skill 4 Animation");
                        break;
                    default:
                        Debug.LogError("Boss Unknown Cast: " + visitor["args"]["skill-index"]);
                        break;
                }
                break;
            default:
                Debug.LogError("Unknown Event: " + visitor["ev"]["type"]);
                break;
        }
    }
}
