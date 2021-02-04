using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _characterPointsText;
    
    private int _characterPoints;

    private StatModifier[] _statModifiers;
    // Start is called before the first frame update
    void Start()
    {
        _characterPoints = 30;
        updateText();

        _statModifiers = GetComponentsInChildren<StatModifier>();
        GetComponentInChildren<CreateCharacterButton>().GetButton().onClick.AddListener(()=>exportStats());

        foreach (var statModifier in _statModifiers)
        {
            statModifier.GetPlusButton().onClick.AddListener(()=>addStat(statModifier));
            statModifier.GetMinusButton().onClick.AddListener(()=>minusStat(statModifier));
        }
    }

    private void exportStats()
    {
        Dictionary<StatType, float> statSheet = new Dictionary<StatType, float>();

        foreach (var statModifier in _statModifiers)
        {
            statSheet[statModifier.GetStatType()] = statModifier.GetStat();
        }
        
        GameStateMachine.Instance.loadStats(statSheet);
        Debug.Log("Stats Loaded");
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
