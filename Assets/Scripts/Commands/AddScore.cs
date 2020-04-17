using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : ICommand
{
    private int scoreAdd; 

    public AddScore(int val)
    {
        scoreAdd = val;
    }
    public void Execute()
    {
        Player.Instance.AddScore(scoreAdd);
        UIController.Instance.UpdateScore(Player.Instance.Score.ToString());
    }
}
