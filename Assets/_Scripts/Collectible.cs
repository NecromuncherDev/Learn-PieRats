using System;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected?.Invoke();
        Destroy(gameObject);
    }
}
