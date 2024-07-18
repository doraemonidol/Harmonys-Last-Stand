using System.Collections;
using System.Collections.Generic;
using Presentation;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossMovement : PresentationObject
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _animator.SetTrigger("Move");
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
