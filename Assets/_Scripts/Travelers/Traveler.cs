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

    protected virtual void StartMove(Vector2 direction)
    {
        OnStartMove?.Invoke();
        moveDir = direction.normalized;
        rotatingBody.up = moveDir;
    }
    protected virtual void StopMove(Vector2 direction)
    {
        OnStopMove?.Invoke();
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (moveDir != Vector2.zero)
            transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
