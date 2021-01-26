using UnityEngine;

public static class LootSystem
{
    private static LootItemHolder _lootItemHolderPrefab;

    public static void Drop(Item item, Transform droppingTransform)
    {
        var lootItemHolder = GetLootItemHolder();
        lootItemHolder.TakeItem(item);

        Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle * 3f;
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

        lootItemHolder.transform.position = randomPosition;
    }

    private static LootItemHolder GetLootItemHolder()
    {
        if(_lootItemHolderPrefab == null)
            _lootItemHolderPrefab = Resources.Load<LootItemHolder>("LootItemHolder");

        LootItemHolder lootItemHolder = GameObject.Instantiate(_lootItemHolderPrefab);
        return lootItemHolder;
    }
}