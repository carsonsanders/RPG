using System.Collections;
using NSubstitute.ClearExtensions;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryPanel : MonoBehaviour
{
    private Inventory _inventory;
    
    public UIInventorySlot[] Slots;

    public int SlotCount => Slots.Length;
    public UIInventorySlot Selected { get; private set; }

    private void Awake()
    {
        Slots = GetComponentsInChildren<UIInventorySlot>();
        RegisterSlotsForClickCallback();
    }

    private void RegisterSlotsForClickCallback()
    {
        foreach (var slot in Slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;
        }
    }

    private void HandleSlotClicked(UIInventorySlot slot)
    {
        Selected = slot;
    }
    
    public void Bind(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.ItemPickedUp -= HandleItemPickedUp;
            _inventory.OnItemChanged -= HandleItemChanged;
        }
        
        _inventory = inventory;
        
        if(_inventory != null)
        {
            _inventory.ItemPickedUp += HandleItemPickedUp;
            _inventory.OnItemChanged += HandleItemChanged;
            RefreshSlots();
        }
        else
        {
            ClearSlots();
        }
    }

    private void HandleItemChanged(int slotNumber)
    {
        Slots[slotNumber].setItem(_inventory.GetItemInSlot(slotNumber));
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