using UnityEngine;

public class Dead : IState
{
    const float DESPAWN_DELAY = 5f;
    
    private float _despawnTime;
    private readonly Entity _entity;

    public Dead(Entity entity)
    {
        _entity = entity;
    }
    public void Tick()
    {
        if(Time.time >= _despawnTime)
            GameObject.Destroy(_entity.gameObject);
    }

    public void OnEnter()
    {
        // Drop loot, 
        _despawnTime = Time.time + DESPAWN_DELAY;
    }

    public void OnExit()
    {
        
    }
}