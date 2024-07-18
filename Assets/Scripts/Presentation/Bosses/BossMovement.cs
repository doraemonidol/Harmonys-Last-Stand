using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Presentation;
using Presentation.Bosses;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[Serializable]
public abstract class BossMovement : PresentationObject
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject player;

    protected Animator animator;
    [Header("Enemy Base")]
    [SerializeField] protected float attackRange;
    
    [SerializeField] protected List<EnemyCollision> enemyCollisions;

    public override void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator.SetTrigger(EnemyActionType.Move);
    }

    public void UpdateEnemyCollision()
    {
        foreach (var enemyCollision in enemyCollisions)
        {
            enemyCollision.LogicHandle = LogicHandle;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}
