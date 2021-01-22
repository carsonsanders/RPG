using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public IPlayerInput playerInput { get; set; }= new PlayerInput();
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementInput = new Vector3(playerInput.Horizontal, 0, playerInput.Vertical);
        Vector3 movement = transform.rotation * movementInput;
        characterController.SimpleMove(movement);
    }
}

public class PlayerInput : IPlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
}

public interface IPlayerInput
{
    float Vertical { get; }
    float Horizontal { get; }
}
