using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private CrosshairMode _crosshairMode;
    [SerializeField] private UseAction[] _actions = new UseAction[0];
    public UseAction[] Actions => _actions;
    public CrosshairMode CrosshairMode => _crosshairMode;

    private bool _wasPickedUp;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_wasPickedUp)
            return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            _wasPickedUp = true;
        }
    }

    private void OnValidate()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
}