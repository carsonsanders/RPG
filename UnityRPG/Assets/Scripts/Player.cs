using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public PlayerInput playerInput { get; }= new PlayerInput();
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementInput = new Vector3(0, 0, playerInput.Vertical);
        Vector3 movement = transform.rotation * movementInput;
        characterController.SimpleMove(movement);
    }
}

public class PlayerInput
{
    public float Vertical { get; set; }//=> Input.GetAxis("Vertical");
}
