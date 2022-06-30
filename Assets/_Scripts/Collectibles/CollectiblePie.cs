using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePie : Collectible
{
    public static Action<int, Transform> OnCollectedPies;
    protected int pies;

    private void Awake()
    {
        pies = UnityEngine.Random.Range(1, 5); // TODO: Change hardcoded values
    }

    protected override void GetCollected(Transform collector)
    {
        OnCollectedPies?.Invoke(pies, collector);
        base.GetCollected(collector);
    }
}
