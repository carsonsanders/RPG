using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;
    
    [SerializeField] private Transform _rightHand;
    
    private List<Item> _items = new List<Item>();
    private Transform _itemRoot;
    
    public Item ActiveItem { get; private set; }

    private void Awake()
    {
        _itemRoot = new GameObject(name: "Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        _items.Add(item);
        item.transform.SetParent(_itemRoot);
        ItemPickedUp?.Invoke(item);

        Equip(item);
    }

    public void Equip(Item item)
    {
        Debug.Log($"Equipped Item {item.gameObject.name}");
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
       
        //?. syntax = checks if ActiveItemChanged is null, if not null then invoke
        ActiveItemChanged?.Invoke(ActiveItem);
    }

    
}