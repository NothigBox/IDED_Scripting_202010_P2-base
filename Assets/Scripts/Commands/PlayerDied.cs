using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied : ICommand
{
    public void Execute()
    {
        UIController.Instance.ActivateDeathScreen();
    }
}
