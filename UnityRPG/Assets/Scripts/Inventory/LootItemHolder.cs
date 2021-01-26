using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    
    private Item _item;

    public void TakeItem(Item item)
    {
        _item = item;
        _item.transform.SetParent(_itemTransform);
        _item.transform.localRotation = Quaternion.identity;
        _item.transform.localPosition = Vector3.zero;
        _item.gameObject.SetActive(true);
        _item.WasPickedUp = false;
        _item.OnPickedUp += HandleItemPickedUp;
    }

    private void HandleItemPickedUp()
    {
        LootSystem.AddToPool(this);
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
