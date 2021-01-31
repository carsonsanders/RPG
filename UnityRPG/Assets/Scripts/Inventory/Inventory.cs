using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int GENERAL_INVENTORY_SIZE = 25;
    public event Action<IItem> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;
    public event Action<int> OnItemChanged; 
    
    public event Action<IItem> ItemEquipped;
    public event Action<IItem> ItemUnEquipped;
    
    [SerializeField] private Transform _rightHand;
    
    public List<InventorySlot> Slots = new List<InventorySlot>();
    private Transform _itemRoot;

    public IItem ActiveItem { get; private set; }
    public List<Item> Items => Slots.Select(t => t.Item).ToList(); //PLACEHOLDER
    public int Count => Slots.Count(t => t.Item != null);

    private void Awake()
    {
        for (int i = 0; i < GENERAL_INVENTORY_SIZE; i++)
        {
            Slots.Add(new InventorySlot());
        }

        var slotTypes = (SlotType[]) Enum.GetValues(typeof(SlotType));
        
        
        foreach (var slotType in slotTypes)
        {
            if(slotType != SlotType.General)
                Slots.Add(new InventorySlot(slotType));
        }
        
        
        _itemRoot = new GameObject(name: "Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item, int? slot = null)
    {
        if (slot.HasValue == false)
            slot = FindFirstAvailableSlot();
        
        if (slot.HasValue == false)
            return;
        
        Slots[slot.Value].SetItem(item);
        item.transform.SetParent(_itemRoot);
        ItemPickedUp?.Invoke(item);
        item.WasPickedUp = true;

        Equip(item);
    }

    private int? FindFirstAvailableSlot()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Item == null)
                return i;
        }

        return null;
    }

    public void Equip(IItem item)
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(_itemRoot);
            ActiveItem.gameObject.SetActive(false);
            ItemUnEquipped?.Invoke(ActiveItem);
        }
        
        Debug.Log($"Equipped Item {item.gameObject.name}");
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
        ActiveItem.gameObject.SetActive(true);
       
        //?. syntax = checks if ActiveItemChanged is null, if not null then invoke
        ActiveItemChanged?.Invoke(ActiveItem);
        ItemEquipped?.Invoke(item);
    }


    public Item GetItemInSlot(int slot)
    {
        return Slots[slot].Item;
    }

    public void Move(int sourceSlot, int destinationSlot)
    {
        var destinationItem = Slots[destinationSlot];
        Slots[destinationSlot] = Slots[sourceSlot];
        Slots[sourceSlot] = destinationItem;

        OnItemChanged?.Invoke(destinationSlot);
        OnItemChanged?.Invoke(sourceSlot);
    }
}