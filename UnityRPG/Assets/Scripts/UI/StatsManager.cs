using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _characterPointsText;
    
    private int _characterPoints;
    // Start is called before the first frame update
    void Start()
    {
        _characterPoints = 30;
        updateText();

        StatModifier[] statModifiers = GetComponentsInChildren<StatModifier>();

        foreach (var statModifier in statModifiers)
        {
            statModifier.GetPlusButton().onClick.AddListener(()=>addStat(statModifier));
            statModifier.GetMinusButton().onClick.AddListener(()=>minusStat(statModifier));
        }
        
    }

    private void addStat(StatModifier sm)
    {
        if (_characterPoints > 0 && sm.GetStat() < 10)
        {
            sm.AddStat();
            _characterPoints -= 1;
        }

        updateText();
    }

    private void minusStat(StatModifier sm)
    {
        if (sm.GetStat() > 2)
        {
            sm.MinusStat();
            _characterPoints += 1;
        } 
        
        updateText();
    }

    private void updateText()
    {
        _characterPointsText.text = "CHARACTER POINTS: [ " + _characterPoints + " ]";
    }

   
}
