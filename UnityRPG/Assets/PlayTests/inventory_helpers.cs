﻿using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayTests
{
    public static class inventory_helpers
    {
        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }

            return inventory;
        }
        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems = 0)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanels.prefab");
            var panel =  Object.Instantiate(prefab);
            var inventory = GetInventory(numberOfItems);
            panel.Bind(inventory);
            return panel;
        }
        
        public static Item GetItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/TestItem.prefab");
            return Object.Instantiate(prefab);
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/Prefabs/UI/SelectionCursor.prefab");
            return Object.Instantiate(prefab);
        }

        public static IEnumerator LoadUITestScene()
        {
            var operation = SceneManager.LoadSceneAsync("ItemTests");
            while (operation.isDone == false)
                yield return null;
            
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;
        }
    }

    
}