using System;

public interface IDamageable
{
    event Action<IDamageable> OnDefeated;

    uint maxHP { get; set; }
    uint curHP { get; set; }

    void TakeDamage(uint damage);
}
