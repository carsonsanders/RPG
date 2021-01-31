using System;

public class InventorySlot
{
    public readonly SlotType SlotType;
    public Item Item { get; private set; }

    public event Action ItemChanged;

    public InventorySlot() => SlotType = SlotType.General;
    public InventorySlot(SlotType slotType)
    {
        SlotType = slotType;
    }
    
    public void SetItem(Item item)
    {
        Item = item;
        ItemChanged?.Invoke();
    }
}