using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;

    internal float liveDistance;
    internal Transform liveTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected?.Invoke();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(liveTarget.position, transform.position) > liveDistance)
            Destroy(gameObject);
    }
}
