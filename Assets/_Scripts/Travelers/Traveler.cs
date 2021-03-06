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

    protected Vector2 moveDir = Vector2.zero;

    private Vector2 lastMoveDir = Vector2.zero;

    protected virtual internal void Halt()
    {
        if (moveDir != Vector2.zero)
            StopMove(Vector2.zero);
    }

    protected virtual internal void Resume()
    {
        if (moveDir == Vector2.zero)
            StartMove(lastMoveDir);
    }

    protected virtual void StartMove(Vector2 direction)
    {
        OnStartMove?.Invoke();
        moveDir = direction.normalized;
        rotatingBody.up = moveDir;
    }

    protected virtual void StopMove(Vector2 direction)
    {
        OnStopMove?.Invoke();
        lastMoveDir = moveDir;
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (moveDir != Vector2.zero)
            transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
