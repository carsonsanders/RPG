using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler
{
    public event Action<UIInventorySlot> OnSlotClicked;
    
    [SerializeField] private Image _image;
    
    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;
    public IItem Item { get; private set; }
    public bool IconImageEnabled => _image.enabled;

    public void Clear()
    {
        Item = null;
    }

    public void setItem(IItem item)
    {
        Item = item;
        _image.sprite = item != null ? item.Icon : null;
        _image.enabled = Item != null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var droppedOnSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<UIInventorySlot>();
        if (droppedOnSlot != null)
            droppedOnSlot.OnPointerDown(eventData);
        else
            OnPointerDown(eventData);
    }

    public void OnDrag(PointerEventData eventData) {}
}