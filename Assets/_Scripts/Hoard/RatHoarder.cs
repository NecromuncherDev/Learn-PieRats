using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHoarder : Hoarder
{
    private void OnEnable()
    {
        CollectibleRat.OnCollectedRat += AddRat;
    }

    private void OnDisable()
    {
        CollectibleRat.OnCollectedRat -= AddRat;
    }

    private void AddRat()
    {
        Hoard++;
    }
}
