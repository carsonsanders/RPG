using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
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
            () => Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 5f);
        
        _stateMachine.AddTransition(chasePlayer, attack,
            () => Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 2f);
        
        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}