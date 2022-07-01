using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour, ISpawnable
{
    public event Action<Collectible, Transform> OnCollected;
    public event Action<ISpawnable> OnSpawned;
    public event Action<ISpawnable> OnDestroyed;

    protected internal float liveDistance;
    protected internal Transform liveTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetCollected(collision.transform);
    }

    private void FixedUpdate()
    {
        if (liveTarget != null)
        {
            if (Vector2.Distance(liveTarget.position, transform.position) > liveDistance)
                Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    protected virtual void GetCollected(Transform collector)
    {
        print($"{collector.name} collected {gameObject.name}");
        OnCollected?.Invoke(this, collector);
        Destroy(gameObject);
    }
}
