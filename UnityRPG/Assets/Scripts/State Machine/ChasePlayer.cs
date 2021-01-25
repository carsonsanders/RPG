using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Player _player;

    public ChasePlayer(NavMeshAgent navMeshAgent, Player player)
    {
        _navMeshAgent = navMeshAgent;
        _player = player;
    }
    public void Tick()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}