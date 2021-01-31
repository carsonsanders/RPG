using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats
{
    private Dictionary<StatType, float> _stats = new Dictionary<StatType, float>();

    public Stats()
    {
        Add(StatType.MoveSpeed, 5);
    }
    public void Add(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] += value;
        else
            _stats[statType] = value;
    }

    public float Get(StatType statType)
    {
        return _stats[statType];
    }

    public void Remove(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] -= value;
    }

    public void Bind(Inventory inventory)
    {
        inventory.ItemEquipped += HandleItemEquipped;
        inventory.ItemUnEquipped += HandleItemUnEquipped;
    }

    private void HandleItemEquipped(IItem item)
    {
        if (item.StatMods == null)
            return;
        
        foreach (var statMod in item.StatMods)
        {
            Add(statMod.StatType, statMod.Value);
        }
    }
    
    private void HandleItemUnEquipped(IItem item)
    {
        if (item.StatMods == null)
            return;
        
        foreach (var statMod in item.StatMods)
        {
            Remove(statMod.StatType, statMod.Value);
        }
    }
}
