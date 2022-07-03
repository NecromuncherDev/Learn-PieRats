using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
