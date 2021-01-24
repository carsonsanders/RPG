using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    [SerializeField] private Sprite _gunSprite;
    [SerializeField] private Sprite _invalidSprite;
   
    private Inventory _inventory;

    private void OnEnable()
    {
        _inventory = FindObjectOfType<Inventory>();
        _inventory.ActiveItemChanged += HandleActiveItemChanged;

        if (_inventory.ActiveItem != null)
            HandleActiveItemChanged(_inventory.ActiveItem); 
        else
            _crosshairImage.sprite = _invalidSprite;
    }

    private void OnValidate()
    {
        _crosshairImage = GetComponent<Image>();
    }

    private void HandleActiveItemChanged(Item item)
    {
        switch (item.CrosshairMode)
        {
            case CrosshairMode.Gun: _crosshairImage.sprite = _gunSprite;
                break;
            case CrosshairMode.Invalid: _crosshairImage.sprite = _invalidSprite;
                break;
            
        }
        Debug.Log($"Crosshair detected {item.CrosshairMode}");
    }
}

public enum CrosshairMode
{
    Invalid,
    Gun,
}
