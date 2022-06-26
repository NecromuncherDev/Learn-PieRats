using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieHoarder : Hoarder
{
    private void OnEnable()
    {
        CollectiblePie.OnCollectedPies += AddPies;
    }

    private void OnDisable()
    {
        CollectiblePie.OnCollectedPies -= AddPies;
    }

    private void AddPies(int pies)
    {
        Hoard += pies;
    }
}
