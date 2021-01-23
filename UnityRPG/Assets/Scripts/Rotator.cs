﻿using UnityEngine;

public class Rotator
{
    private readonly Player _player;

    public Rotator(Player player)
    {
        _player = player;
    }

    public void Tick()
    {
        var rotation = new Vector3(0, _player.playerInput.MouseX, 0);
        _player.transform.Rotate(rotation);
    }
}