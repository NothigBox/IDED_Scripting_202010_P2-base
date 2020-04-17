using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : ICommand
{
    public void Execute()
    {
        Player.Instance.TakeDamage();
        UIController.Instance.UpdateLifeImages(Player.Instance.Lives);
    }
}
