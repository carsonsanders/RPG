using System.Collections;
using NSubstitute.ClearExtensions;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    private Inventory _inventory;
    
    public UIInventorySlot[] Slots;


    private void Awake()
    {
        Slots = GetComponentsInChildren<UIInventorySlot>();
    }

    public int SlotCount => Slots.Length;

    public void Bind(Inventory inventory)
    {
        if (_inventory != null)
            _inventory.ItemPickedUp -= HandleItemPickedUp;
        
        _inventory = inventory;
        
        if(_inventory != null)
        {
            _inventory.ItemPickedUp += HandleItemPickedUp;
            RefreshSlots();
        }
        else
        {
            ClearSlots();
        }
    }

    private void ClearSlots()
    {
        foreach (var slot in Slots)
        {
            slot.Clear();
        }
    }

    private void RefreshSlots()
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var slots = Slots[i];
            
            if(_inventory.Items.Count > i)
                slots.setItem(_inventory.Items[i]);
            else
                slots.Clear();
        }
    }

    private void HandleItemPickedUp(Item item)
    {
        RefreshSlots();
    }
}