using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePieRat : CollectiblePie
{
    protected override void GetCollected(Transform collector)
    {
        CollectibleRat.OnCollectedRat?.Invoke(collector);
        base.GetCollected(collector);
    }
}
