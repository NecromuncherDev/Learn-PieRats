using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;

    private void Start()
    {
        GameManager.Instance.player.OnLose += ShowLosePanel;
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
}
