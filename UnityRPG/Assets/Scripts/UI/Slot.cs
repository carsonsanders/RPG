using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private Item _item;
    public bool isEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
        _icon.sprite = item.Icon;
    }
}