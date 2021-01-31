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