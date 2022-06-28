using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatStock : Stock
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
        Add(1);
    }
}
