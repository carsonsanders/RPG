﻿using System;
using UnityEngine;
public class ItemLogger : ItemComponent
{
    public override void Use()
    {
        Debug.Log("Item Was Used");
    }
}