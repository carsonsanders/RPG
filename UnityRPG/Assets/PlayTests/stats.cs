using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEngine;

public class stats
{
    [Test]
    public void can_add()
    {
        Stats stats = new Stats();
        stats.Add(StatType.MoveSpeed, 5f);
        Assert.AreEqual(7f, stats.Get(StatType.MoveSpeed));
        stats.Add(StatType.MoveSpeed, 5f);
        Assert.AreEqual(12f, stats.Get(StatType.MoveSpeed));
    }
    
    [Test]
    public void can_remove()
    {
        Stats stats = new Stats();
        stats.Add(StatType.MoveSpeed, 5f);
        Assert.AreEqual(7f, stats.Get(StatType.MoveSpeed));
        
        stats.Remove(StatType.MoveSpeed, 5f);
        Assert.AreEqual(2f, stats.Get(StatType.MoveSpeed));
    }
}