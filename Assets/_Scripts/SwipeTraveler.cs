using System;
using UnityEngine;

public class SwipeTraveler : Traveler
{
    private TouchDetector touchDetector;
    private Vector2 moveDir = Vector2.zero;

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

    private void Update()
    {
        if (moveDir != Vector2.zero)
            transform.Translate(moveDir * speed * Time.deltaTime);
    }

    protected override void StartMove(Vector2 direction)
    {
        base.StartMove(direction);
        moveDir = direction;
        rotatingBody.up = direction;
    }

    protected override void StopMove(Vector2 direction)
    {
        base.StopMove(direction);
        moveDir = Vector2.zero;
    }
}
