using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePie : Collectible
{
    public static Action<int> OnCollectedPies;
    protected int pies;

    private void Awake()
    {
        pies = UnityEngine.Random.Range(1, 5); // TODO: Change hardcoded values
    }

    protected override void GetCollected()
    {
        OnCollectedPies?.Invoke(pies);
        base.GetCollected();
    }
}
