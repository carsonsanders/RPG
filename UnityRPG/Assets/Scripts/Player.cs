using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private IMover _mover;
    private Rotator _rotator;
    private Inventory _inventory;

    public Stats Stats { get; private set; }
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);//new NavmeshMover(this); //Mover(this);
        _rotator = new Rotator(this);
        _inventory = GetComponent<Inventory>();

        PlayerInput.Instance.MoveModeTogglePressed += MoveModeTogglePressed;

        Stats = new Stats();
        Stats.Bind(_inventory);
        loadStats(GameStateMachine.Instance.GetStats());
        Debug.Log($"INT STAT: {Stats.Get(StatType.INT)}");
    }

    private void loadStats(Dictionary<StatType, float> stats)
    {
        Stats.Add(StatType.INT, stats[StatType.INT]);
        Stats.Add(StatType.REF, stats[StatType.REF]);
        Stats.Add(StatType.TECH, stats[StatType.TECH]);
        Stats.Add(StatType.COOL, stats[StatType.COOL]);
        Stats.Add(StatType.ATTR, stats[StatType.ATTR]);
        Stats.Add(StatType.LUCK, stats[StatType.LUCK]);
        Stats.Add(StatType.BODY, stats[StatType.BODY]);
        Stats.Add(StatType.EMPATHY, stats[StatType.EMPATHY]);
    }

    private void MoveModeTogglePressed()
    {
        if (_mover is NavmeshMover)
            _mover = new Mover(this);
        else
            _mover = new NavmeshMover(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.Active)
            return;
        
        _mover.Tick();
        _rotator.Tick();
    }
}