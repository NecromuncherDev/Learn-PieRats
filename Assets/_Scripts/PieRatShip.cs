using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieHoarder), typeof(RatHoarder), typeof(Traveler))]
public class PieRatShip : MonoBehaviour
{
    private RatHoarder crew;
    private PieHoarder stock;

    private void Awake()
    {
        TryGetComponent<RatHoarder>(out crew);
        TryGetComponent<PieHoarder>(out stock);
    }
}
