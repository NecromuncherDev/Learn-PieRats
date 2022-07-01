using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPieRatShip : PieRatShip
{
    [SerializeField] [Range(1, 100)] private float startingAmmoVariation;
    [SerializeField] [Range(1, 100)] private float startingCrewVariation;

    protected override void Initialize()
    {
        startingAmmo = (uint)(startingAmmo + ((Random.Range(-startingAmmoVariation, startingAmmoVariation) / 100) * (int)startingAmmo));
        startingCrew = (uint)(startingCrew + ((Random.Range(-startingCrewVariation, startingCrewVariation) / 100) * (int)startingCrew));
        base.Initialize();
    }
}
