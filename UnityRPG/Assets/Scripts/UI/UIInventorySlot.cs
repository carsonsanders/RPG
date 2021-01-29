using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<UIInventorySlot> OnSlotClicked;
    
    [SerializeField] private Image _image;
    
    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;
    public IItem Item { get; private set; }

    public void Clear()
    {
        Item = null;
    }

    public void setItem(IItem item)
    {
        Item = item;
        _image.sprite = item != null ? item.Icon : null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }
}