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
        //playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _mover = new Mover(this);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _mover = new NavmeshMover(this);
        
        _mover.Tick();
        _rotator.Tick();
    }
}