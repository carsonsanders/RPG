using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats
{
    private Dictionary<StatType, float> _stats = new Dictionary<StatType, float>();

    public void Add(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] += value;
        else
            _stats[statType] = value;
    }

    public double Get(StatType statType)
    {
        return _stats[statType];
    }

    public void Remove(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] -= value;
    }
}
