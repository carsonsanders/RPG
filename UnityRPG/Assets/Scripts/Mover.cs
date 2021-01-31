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
        Vector3 movementInput = new Vector3(PlayerInput.Instance.Horizontal, 0, PlayerInput.Instance.Vertical);
        Vector3 movement = _player.transform.rotation * movementInput * _player.Stats.Get(StatType.MoveSpeed);
        characterController.SimpleMove(movement);
    }
}