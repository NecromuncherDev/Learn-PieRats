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

    protected internal override void Halt()
    {
        base.Halt();
        touchDetector.OnSwipeDetected -= StartMove;
    }

    protected internal override void Resume()
    {
        base.Resume();
        touchDetector.OnSwipeDetected += StartMove;
    }
}
