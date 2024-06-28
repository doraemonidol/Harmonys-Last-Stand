using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
