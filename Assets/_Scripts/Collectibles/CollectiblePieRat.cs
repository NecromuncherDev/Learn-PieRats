using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePieRat : CollectiblePie
{
    protected override void GetCollected()
    {
        CollectibleRat.OnCollectedRat?.Invoke();
        base.GetCollected();
    }
}
