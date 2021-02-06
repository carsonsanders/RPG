using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Player player);
    string GetActionText();
    bool canInteract();
    event Action PopUp;
}
