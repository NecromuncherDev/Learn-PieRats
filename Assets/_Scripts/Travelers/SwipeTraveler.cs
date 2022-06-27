using System;
using UnityEngine;

public class SwipeTraveler : Traveler
{
    private TouchDetector touchDetector;

    private void Awake()
    {
        touchDetector = TouchDetector.Instance;
    }

    private void OnEnable()
    {
        touchDetector.OnSwipeDetected += StartMove;
        touchDetector.OnTapDetected += StopMove;
    }

    private void OnDisable()
    {
        touchDetector.OnSwipeDetected -= StartMove;
        touchDetector.OnTapDetected -= StopMove;
    }
}
