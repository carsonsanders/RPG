using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateCharacterButton : MonoBehaviour
{
    public static string LevelToLoad;
    
    [SerializeField] private string _levelName;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => LevelToLoad = _levelName);
    }

    public Button GetButton()
    {
        return _button;
    }
}