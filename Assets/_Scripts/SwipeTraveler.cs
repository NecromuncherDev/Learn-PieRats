using System;
using UnityEngine;

public class SwipeTraveler : MonoBehaviour
{
    [SerializeField] float speed;

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
    private void StartMove(Vector2 direction)
    {
        moveDir = direction;
    }

    private void StopMove(Vector2 position)
    {
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (moveDir != Vector2.zero)
            transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
