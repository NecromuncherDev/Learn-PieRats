using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    public event Action<Collectible> OnCollected;
    public event Action<Collectible> OnDestroyed;

    protected internal float liveDistance;
    protected internal Transform liveTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetCollected();
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(liveTarget.position, transform.position) > liveDistance)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    protected virtual void GetCollected()
    {
        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
