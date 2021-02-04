using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatModifier : MonoBehaviour
{
    [SerializeField] private StatType statType;
    private int _statValue;
    private TMPro.TextMeshProUGUI _valueString;
    private Button _plusButton;
    private Button _minusButton;

    public void AddStat()
    {
        _statValue += 1;
        updateText();
    }

    public void MinusStat()
    {
        _statValue -= 1;
        updateText();
    }

    public int GetStat()
    {
        return _statValue;
    }
    

    public Button GetPlusButton()
    {
        return _plusButton;
    }

    public Button GetMinusButton()
    {
        return _minusButton;
    }

    private void updateText()
    {
        _valueString.text = "[ " + _statValue + " ]";
    }
    
    public StatType GetStatType()
    {
        return statType;
    }
    

    private void OnEnable()
    {
        _valueString = GetComponent<TMPro.TextMeshProUGUI>();
        Button[] buttons = GetComponentsInChildren<Button>();
        _statValue = 2;
        _plusButton = buttons[0];
        _minusButton = buttons[1];
    }
}
