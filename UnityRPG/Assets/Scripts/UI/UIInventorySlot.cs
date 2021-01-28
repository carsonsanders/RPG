using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    private Item _item;
    public bool IsEmpty => _item == null;

    public void Clear()
    {
        _item = null;
    }

    public void setItem(Item item)
    {
        _item = item;
    }
}