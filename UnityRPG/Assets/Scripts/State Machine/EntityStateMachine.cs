using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;

    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        _stateMachine = new StateMachine();

        var idle = new Idle();
        var chasePlayer = new ChasePlayer(_navMeshAgent);
        var attack = new Attack();

        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);

        _stateMachine.AddTransition(idle, chasePlayer,
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) < 5f);
        
        _stateMachine.AddTransition(chasePlayer, attack,
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) < 2f);
        
        _stateMachine.SetState(idle);
    }

    private float DistanceFlat(Vector3 source, Vector3 destination)
    {
        return Vector3.Distance(new Vector3(source.x, 0, source.z), new Vector3(destination.x, 0, destination.z));
    }
    
    private void Update()
    {
        _stateMachine.Tick();
    }
}