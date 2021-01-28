using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    private IItem _item;
    public bool IsEmpty => _item == null;
    public Sprite Icon => _image.sprite;

    public void Clear()
    {
        _item = null;
    }

    public void setItem(IItem item)
    {
        _item = item;
        _image.sprite = item != null ? item.Icon : null;
    }
}