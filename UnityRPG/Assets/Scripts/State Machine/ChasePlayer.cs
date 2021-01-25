using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;

    public ChasePlayer(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
    }
    public void Tick()
    {
        
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