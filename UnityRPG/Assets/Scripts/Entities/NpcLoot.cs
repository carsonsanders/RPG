using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class NpcLoot : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;
    
    private Inventory _inventory;
    private EntityStateMachine _entityStateMachine;

    //Start occurs after awake, need it here to reference inventory
    private void Start()
    {
        _entityStateMachine = GetComponent<EntityStateMachine>();
        _entityStateMachine.OnEntityStateChanged += HandleEntityStateChanged;
        
        _inventory = GetComponent<Inventory>();

        foreach (var itemPrefab in _itemPrefabs)
        {
            var itemInstance = GameObject.Instantiate(itemPrefab);
            _inventory.Pickup(itemInstance);
        }
        
    }

    private void HandleEntityStateChanged(IState state)
    {
        Debug.Log($"HandleEntityStateChanged {state.GetType()}");
        if (state is Dead)
            DropLoot();
    }

    private void DropLoot()
    {
        foreach (var item in _inventory.Items)
        {
            item.transform.SetParent(null);
            item.transform.position = transform.position + transform.right;
            item.gameObject.SetActive(true);
            item.WasPickedUp = false;
        }

        _inventory.Items.Clear();
    }
}
