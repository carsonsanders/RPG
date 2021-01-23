using UnityEngine;
using UnityEngine.AI;

public class Mover : IMover
{
    private readonly Player _player;
    private readonly CharacterController characterController;

    public Mover(Player player)
    {
        _player = player;
        characterController = player.GetComponent<CharacterController>();
        _player.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Tick()
    {
        Vector3 movementInput = new Vector3(_player.playerInput.Horizontal, 0, _player.playerInput.Vertical);
        Vector3 movement = _player.transform.rotation * movementInput;
        characterController.SimpleMove(movement);
    }
}