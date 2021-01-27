using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private IMover _mover;
    private Rotator _rotator;
    public IPlayerInput playerInput { get; set; }= new PlayerInput();
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);//new NavmeshMover(this); //Mover(this);
        _rotator = new Rotator(this);

        playerInput.MoveModeTogglePressed += MoveModeTogglePressed;
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
        playerInput.Tick();
    }
}