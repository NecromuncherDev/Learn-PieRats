using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieStock : Stock
{
    private void OnEnable()
    {
        CollectiblePie.OnCollectedPies += AddPies;
    }

    private void OnDisable()
    {
        CollectiblePie.OnCollectedPies -= AddPies;
    }

    private void AddPies(int pies, Transform collector)
    {
        if (transform == collector.parent)
            Add((uint)Mathf.Max(0, pies));
    }
}
