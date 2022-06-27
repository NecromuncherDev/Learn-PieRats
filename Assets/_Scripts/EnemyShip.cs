using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieHoarder)), RequireComponent(typeof(RatHoarder)), RequireComponent(typeof(Traveler))]
public class EnemyShip : MonoBehaviour
{
    private RatHoarder crew;
    private PieHoarder stock;

    private void Awake()
    {
        TryGetComponent<RatHoarder>(out crew);
        TryGetComponent<PieHoarder>(out stock);
    }
}
