using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRat : Collectible
{
    public static Action<Transform> OnCollectedRat;
    private float floatTime;

    private void Start()
    {
        floatTime = UnityEngine.Random.Range(15f, 30f); // TODO: Change hardcoded values
        Destroy(gameObject, floatTime);
    }

    protected override void GetCollected(Transform collector)
    {
        OnCollectedRat?.Invoke(collector);
        base.GetCollected(collector);
    }
}
