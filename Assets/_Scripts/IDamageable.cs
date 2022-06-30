using System;
using UnityEngine;

public interface IDamageable
{
    event Action<GameObject> OnDefeated;

    uint maxHP { get; set; }
    uint curHP { get; set; }

    void TakeDamage(uint damage);
}
