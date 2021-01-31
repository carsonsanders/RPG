using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using NSubstitute.ClearExtensions;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryPanel : MonoBehaviour
{
    public event Action OnSelectionChanged;
    private Inventory _inventory;
    
    public UIInventorySlot[] Slots;

    public int SlotCount => Slots.Length;
    public UIInventorySlot Selected { get; private set; }

    public bool IsSelected => Selected != null;

    private void Awake()
    {
        Slots = FindObjectsOfType<UIInventorySlot>()
            .OrderByDescending(t =>t.SortIndex)
            .ThenBy(t => t.name)
            .ToArray();
        
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
        if (Selected != null)
        {
            if(SlotCanHoldItem(slot, Selected.Item))
            {
                Swap(slot);
                Selected.BecomeUnSelected();
                Selected = null;
            }
        }
        else if (slot.IsEmpty == false)
        {
            Selected = slot;
            Selected.BecomeSelected();

        }

        OnSelectionChanged?.Invoke();
    }

    private bool SlotCanHoldItem(UIInventorySlot slot, IItem selectedItem)
    {
        return slot.SlotType == SlotType.General || slot.SlotType == selectedItem.SlotType;
    }

    private void Swap(UIInventorySlot slot)
    {
        _inventory.Move(GetSlotIndex(Selected), GetSlotIndex(slot));
    }

    private int GetSlotIndex(UIInventorySlot selected)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (Slots[i] == selected)
            {
                return i;
            }
            
        }
        return -1; //shouldn't happen
    }

    public void Bind(Inventory inventory)
    {
        var availableUiSlots = Slots.OrderBy(t => t.SortIndex).ToList();
        
        if (_inventory != null)
        {
            RemoveExistingInventoryBindings();
        }
        
        _inventory = inventory;
        
        if(_inventory != null)
        {
            for (int i = 0; i < _inventory.Slots.Count; i++)
            {
                var inventorySlot = _inventory.Slots[i];
                Debug.Log($"Slot: {i}, SlotType: {inventorySlot.SlotType}, ");
                var uiSlot = availableUiSlots.FirstOrDefault(t => t.SlotType == inventorySlot.SlotType);
                if(uiSlot != null)
                    uiSlot.Bind(inventorySlot);
            }
            
            
            _inventory.ItemPickedUp += HandleItemPickedUp;
            _inventory.OnItemChanged += HandleItemChanged;
            RefreshSlots();
        }
        else
        {
            ClearSlots();
        }
    }

    private void RemoveExistingInventoryBindings()
    {
        _inventory.ItemPickedUp -= HandleItemPickedUp;
        _inventory.OnItemChanged -= HandleItemChanged;
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