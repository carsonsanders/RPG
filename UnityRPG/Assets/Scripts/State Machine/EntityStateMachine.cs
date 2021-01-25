using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _stateMachine = new StateMachine();

        var idle = new Idle();
        var chasePlayer = new ChasePlayer(_navMeshAgent);
        var attack = new Attack();

        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);
        
        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}