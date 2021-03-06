﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;
        DrawSlotType(item);
        DrawIcon(item);
        DrawCrosshair(item);
        DrawActions(item);
        DrawStatmods(item);

        //base.OnInspectorGUI();
    }

    private void DrawSlotType(object item)
    {
        using (var slotTypeProperty = serializedObject.FindProperty("_slotType"))
        {
            slotTypeProperty.enumValueIndex =
                (int) (SlotType) EditorGUILayout.EnumPopup((SlotType) slotTypeProperty.enumValueIndex,
                    GUILayout.Width(120));
        }
    }

    private void DrawStatmods(Item item)
    {
        using (var statModsProperty = serializedObject.FindProperty("_statMods"))
        {
            for (int i = 0; i < statModsProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    statModsProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    break;
                }

                var statMod = statModsProperty.GetArrayElementAtIndex(i);
                if (statMod != null)
                {
                    var statType = statMod.FindPropertyRelative("StatType");
                    var valueProperty = statMod.FindPropertyRelative("Value");

                    statType.enumValueIndex =
                        (int) (StatType) EditorGUILayout.EnumPopup((StatType) statType.enumValueIndex,
                            GUILayout.Width(120));
                    EditorGUILayout.PropertyField(valueProperty, GUIContent.none, false);
                    
                    serializedObject.ApplyModifiedProperties();
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ Add Stat"))
            {
                statModsProperty.InsertArrayElementAtIndex((statModsProperty.arraySize));
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
    private void DrawActions(Item item)
    {
        using (var actionsProperty = serializedObject.FindProperty("_actions"))
        {
            for (int i = 0; i < actionsProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    actionsProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    break;
                }

                var action = actionsProperty.GetArrayElementAtIndex(i);
                if (action != null)
                {
                    var useModeProperty = action.FindPropertyRelative("UseMode");
                    var targetComponentProperty = action.FindPropertyRelative("TargetComponent");

                    useModeProperty.enumValueIndex =
                        (int) (UseMode) EditorGUILayout.EnumPopup((UseMode) useModeProperty.enumValueIndex,
                            GUILayout.Width(80));
                    EditorGUILayout.PropertyField(targetComponentProperty, GUIContent.none, false);
                    
                    serializedObject.ApplyModifiedProperties();
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Auto Assign Actions"))
            {
                List<ItemComponent> assignedItemComponents = new List<ItemComponent>();
                for (int i = 0; i < actionsProperty.arraySize; i++)
                {
                    var action = actionsProperty.GetArrayElementAtIndex(i);
                    if (action != null)
                    {
                        var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                        var assignedItemComponent = targetComponentProperty.objectReferenceValue as ItemComponent;
                        assignedItemComponents.Add(assignedItemComponent);
                    }
                }

                foreach (var itemComponent in item.GetComponentsInChildren<ItemComponent>())
                {
                    if(assignedItemComponents.Contains(itemComponent))
                        continue;
                    
                    actionsProperty.InsertArrayElementAtIndex((actionsProperty.arraySize));
                    var action = actionsProperty.GetArrayElementAtIndex(actionsProperty.arraySize - 1);
                    var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                    targetComponentProperty.objectReferenceValue = itemComponent;
                    serializedObject.ApplyModifiedProperties();
                }
                
            }
        }
    }

    private void DrawCrosshair(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Crosshair",GUILayout.Width(120) );
        if(item.CrosshairDefinition?.Sprite != null)
        {
            GUILayout.Box(item.CrosshairDefinition.Sprite.texture, GUILayout.Width(60), GUILayout.Height(60));
        }
        else
        {
            EditorGUILayout.HelpBox("No Crosshair Selected", MessageType.Warning);
        }

        using (var property = serializedObject.FindProperty("_crosshairDefinition"))
        {
            var crosshairDefinition = (CrosshairDefinition) EditorGUILayout.ObjectField(item.CrosshairDefinition, typeof(CrosshairDefinition), false);
            property.objectReferenceValue = crosshairDefinition;
            serializedObject.ApplyModifiedProperties();
        }
        
        EditorGUILayout.EndHorizontal();
    }

    private void DrawIcon(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Icon",GUILayout.Width(120) );
        if(item.Icon != null)
        {
            GUILayout.Box(item.Icon.texture, GUILayout.Width(60), GUILayout.Height(60));
        }
        else
        {
            EditorGUILayout.HelpBox("No Icon Selected", MessageType.Warning);
        }

        using (var property = serializedObject.FindProperty("_icon"))
        {
            var sprite = (Sprite) EditorGUILayout.ObjectField(item.Icon, typeof(Sprite), false);
            property.objectReferenceValue = sprite;
            serializedObject.ApplyModifiedProperties();
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    
}