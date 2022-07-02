using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PieRatShip player;

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
        print("You lose");
    }
}
