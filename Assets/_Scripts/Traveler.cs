using System;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    #region Events
    public event Action OnStartMove;
    public event Action OnStopMove;
    #endregion

    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Transform rotatingBody;

    protected virtual void StartMove(Vector2 direction)
    {
        OnStartMove?.Invoke();
    }
    protected virtual void StopMove(Vector2 direction)
    {
        OnStopMove?.Invoke();
    }
}
