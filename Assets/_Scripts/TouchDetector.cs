using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TouchDetector : Singleton<TouchDetector>
{
    #region Events
    public delegate void SwipeDetectedEvent(Vector2 direction);
    public event SwipeDetectedEvent OnSwipeDetected;

    public delegate void TapDetectedEvent(Vector2 position);
    public event TapDetectedEvent OnTapDetected;
    #endregion

    [SerializeField] private float maxDuration = 1f;
    [SerializeField] private float minDistance = 0.2f;

    private InputManager inputManager;
    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    protected override void Awake()
    {
        base.Awake();
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += TouchStart;
        inputManager.OnEndTouch += TouchEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= TouchStart;
        inputManager.OnEndTouch -= TouchEnd;
    }

    private void TouchStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void TouchEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;

        DetermineTouchType();
    }

    private void DetermineTouchType()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance &&
            (endTime - startTime <= maxDuration))
        {
            print("Swipe detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

            Vector2 direction = (endPosition - startPosition).normalized;
            print($"Swipe detected: {direction}");

            OnSwipeDetected?.Invoke(direction);
        }
        else if (Vector3.Distance(startPosition, endPosition) <= minDistance)
        {
            print("Tap detected");
            OnTapDetected?.Invoke(startPosition);
        }
    }
}
