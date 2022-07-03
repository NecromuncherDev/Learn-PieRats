using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal event Action OnEndGame;
    
    [SerializeField] internal PieRatShip player;

    private void OnEnable()
    {
        player.OnLose += EndGame;
    }

    private void OnDisable()
    {
        player.OnLose -= EndGame;
    }

    private void EndGame()
    {
        OnEndGame?.Invoke();
    }
}
