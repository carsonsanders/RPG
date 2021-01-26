using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab;
    
    private static LootSystem _instance;

    private void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
        
    }

    public static void Drop(Item item, Transform droppingTransform)
    {
        _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
        
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        var operation = _lootItemHolderPrefab.InstantiateAsync();
        yield return operation;
        
        var lootItemHolder = operation.Result.GetComponent<LootItemHolder>();;
        lootItemHolder.TakeItem(item);

        Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle * 3f;
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

        lootItemHolder.transform.position = randomPosition;
    }

}