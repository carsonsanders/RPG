using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int DEFAULT_INVENTORY_SIZE = 25;
    public event Action<Item> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;
    public event Action<int> OnItemChanged; 
    
    [SerializeField] private Transform _rightHand;
    
    private Item[] _items = new Item[DEFAULT_INVENTORY_SIZE];
    private Transform _itemRoot;
    
    public Item ActiveItem { get; private set; }
    public List<Item> Items => _items.ToList(); //PLACEHOLDER
    public int Count => _items.Count(t => t != null);

    private void Awake()
    {
        _itemRoot = new GameObject(name: "Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item, int? slot = null)
    {
        if (slot.HasValue == false)
            slot = FindFirstAvailableSlot();
        
        if (slot.HasValue == false)
            return;
        
        _items[slot.Value] = item;
        item.transform.SetParent(_itemRoot);
        ItemPickedUp?.Invoke(item);
        item.WasPickedUp = true;

        Equip(item);
    }

    private int? FindFirstAvailableSlot()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
                return i;
        }

        return null;
    }

    public void Equip(Item item)
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(_itemRoot);
            ActiveItem.gameObject.SetActive(false);
        }
        
        Debug.Log($"Equipped Item {item.gameObject.name}");
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
        ActiveItem.gameObject.SetActive(true);
       
        //?. syntax = checks if ActiveItemChanged is null, if not null then invoke
        ActiveItemChanged?.Invoke(ActiveItem);
    }


    public Item GetItemInSlot(int slot)
    {
        return _items[slot];
    }

    public void Move(int sourceSlot, int destinationSlot)
    {
        var destinationItem = _items[destinationSlot];
        _items[destinationSlot] = _items[sourceSlot];
        _items[sourceSlot] = destinationItem;

        OnItemChanged?.Invoke(destinationSlot);
        OnItemChanged?.Invoke(sourceSlot);
    }
}