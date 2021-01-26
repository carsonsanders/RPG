using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    
    public void TakeItem(Item item)
    {
        item.transform.SetParent(_itemTransform);
        item.transform.localRotation = Quaternion.identity;
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(true);
        item.WasPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
