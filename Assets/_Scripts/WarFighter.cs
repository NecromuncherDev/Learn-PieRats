using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarFighter : MonoBehaviour
{
    internal event Action<WarMonger> OnJoinedWar;

    private void OnEnable()
    {
        WarMonger.OnWarAccepted += CheckForWar;
    }

    private void OnDisable()
    {
        WarMonger.OnWarAccepted -= CheckForWar;
    }

    private void CheckForWar(WarMonger ship1, WarMonger ship2)
    {
        if (ship1.gameObject == gameObject || ship2.gameObject == gameObject)
        {
            OnJoinedWar?.Invoke(ship1.gameObject == gameObject ? ship2 : ship1);
            print($"{gameObject.name} has joined the war!");
        }
    }
}
