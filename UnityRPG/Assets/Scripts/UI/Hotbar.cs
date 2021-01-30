﻿using System;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private Inventory _inventory;
    private Slot[] _slots;
    private Player _player;

    private void OnEnable()
    {
        PlayerInput.Instance.HotkeyPressed += HotkeyPressed;
        
        _inventory = FindObjectOfType<Inventory>();
        _slots = GetComponentsInChildren<Slot>();
    }

    private void OnDisable()
    {
        PlayerInput.Instance.HotkeyPressed -= HotkeyPressed;
    }

    private void HotkeyPressed(int index)
    {

        if (index >= _slots.Length || index < 0)
            return;
        
        if (_slots[index].isEmpty == false)
        {
            _inventory.Equip(_slots[index].Item);
        }
    }
}