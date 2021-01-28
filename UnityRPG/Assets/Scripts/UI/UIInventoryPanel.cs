using System.Collections;
using NSubstitute.ClearExtensions;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public UIInventorySlot[] Slots;


    private void Awake()
    {
        Slots = GetComponentsInChildren<UIInventorySlot>();
    }

    public int SlotCount => Slots.Length;

    public void Bind(Inventory inventory)
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var slots = Slots[i];
            
            if(inventory.Items.Count > i)
                slots.setItem(inventory.Items[i]);
            else
                slots.Clear();
        }
    }
}