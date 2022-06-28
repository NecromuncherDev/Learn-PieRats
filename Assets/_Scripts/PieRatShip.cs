using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieStock), typeof(RatStock), typeof(Traveler))]
public class PieRatShip : MonoBehaviour
{
    private RatStock crew;
    private PieStock stock;

    private void Awake()
    {
        TryGetComponent<RatStock>(out crew);
        TryGetComponent<PieStock>(out stock);
    }
}
