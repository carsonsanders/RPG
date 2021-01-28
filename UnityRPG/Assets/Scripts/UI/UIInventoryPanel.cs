using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    private UIInventorySlot[] _slots;


    private void Awake()
    {
        _slots = GetComponentsInChildren<UIInventorySlot>();
    }

    public int SlotCount => _slots.Length;
}