using UnityEngine;

public class ItemRaycaster : ItemComponent
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] float _range = 10f;
    [SerializeField] private int _damage = 1;
    
    private RaycastHit[] _results = new RaycastHit[100];
    private int _layerMask;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Default");
    }

    //Shoots a ray from mid camera, creates cubes on collision hits
    public override void Use()
    {
        _nextUseTime = Time.time + _delay;

        // returns any number of hits along our ray, put into _results within range/layer 
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f); //ray in middle of camview
        int hits = Physics.RaycastNonAlloc(ray, _results, _range, _layerMask, QueryTriggerInteraction.Collide);

        RaycastHit nearest = new RaycastHit();
        double nearestDistance = double.MaxValue;
        for (int i = 0; i < hits; i++)
        {
            var distance = Vector3.Distance(transform.position, _results[i].point);
            if (distance < nearestDistance)
            {
                nearest = _results[i];
                nearestDistance = distance;
            }
            
        }
        if(nearest.transform != null)
        {
            var takeHits = nearest.collider.GetComponent<ITakeHits>();
            takeHits?.TakeHit(_damage);
        }
    }
}