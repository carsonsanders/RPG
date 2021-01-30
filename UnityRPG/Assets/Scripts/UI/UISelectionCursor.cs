using System;
using UnityEngine;

public class UISelectionCursor : MonoBehaviour
{
    private UIInventoryPanel _inventoryPanel;
    public bool IconVisible { get; set; }

    private void Awake()
    {
        _inventoryPanel = FindObjectOfType<UIInventoryPanel>();
    }

    private void OnEnable()
    {
        _inventoryPanel.OnSelectionChanged += HandleSelectionChanged;
    }

    private void OnDisable()
    {
        _inventoryPanel.OnSelectionChanged -= HandleSelectionChanged;
    }

    private void HandleSelectionChanged()
    {
        IconVisible = !_inventoryPanel.Selected.IsEmpty;
    }
}