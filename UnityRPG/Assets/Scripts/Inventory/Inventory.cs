using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();
    private Transform _itemRoot;

    private void Awake()
    {
        _itemRoot = new GameObject(name: "Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        _items.Add(item);
        item.transform.SetParent(_itemRoot);

        Equip(item);
    }

    private void Equip(Item item)
    {
        Debug.Log($"Equipped Item {item.gameObject.name}");
    }
}