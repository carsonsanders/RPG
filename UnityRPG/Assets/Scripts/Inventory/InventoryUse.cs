﻿using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryUse : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (_inventory.ActiveItem == null)
            return;

        foreach (var useAction in _inventory.ActiveItem.Actions)
        {
            if(useAction.TargetComponent.CanUse && WasPressed(useAction.UseMode))
            {
                useAction.TargetComponent.Use();
            }
        }
    }

    private bool WasPressed(UseMode useMode)
    {
        switch (useMode)
        {
            case UseMode.LeftClick: return Input.GetMouseButtonDown(0);
            case UseMode.RightClick: return Input.GetMouseButtonDown(1);
            case UseMode.Spacebar: return Input.GetKeyDown(KeyCode.Space);
        }

        return false;
    }
}