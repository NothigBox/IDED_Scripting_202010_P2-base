using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private ICommand playerHit;
    private ICommand addScore;
    private ICommand playerDied;

    private void Awake()
    {
        playerHit = new PlayerHit();
        playerDied = new PlayerDied();

        UnsuscribeEvents();
        
        SuscribeEvents();
    }

    private void PHit()
    {
        if(playerHit != null) playerHit.Execute();
    }

    private void PScoreAdd(int scoreAdd)
    {
        addScore = new AddScore(scoreAdd);
        addScore.Execute();
    }
    private void PDied()
    {
        UnsuscribeEvents();
        if(playerDied != null) playerDied.Execute();
    }

    private void UnsuscribeEvents()
    {
        Player.OnPlayerHit -= PHit;
        Player.OnPlayerScoreChanged -= PScoreAdd;
        Player.OnPlayerDied -= PDied;
    }
    private void SuscribeEvents()
    {
        Player.OnPlayerHit += PHit;
        Player.OnPlayerScoreChanged += PScoreAdd;
        Player.OnPlayerDied += PDied;
    }
}
