using System;
using System.Collections;
using UnityEngine;

public class CrewMember : MonoBehaviour, IDamageable
{
    public event Action<GameObject> OnDefeated;
    public uint maxHP { get => _maxHP; set => _maxHP = value; }
    public uint curHP { get => _curHP; set => _curHP = (uint)Mathf.Min(value, maxHP); }

    [SerializeField] private float throwSpeed;

    private Projectile ammoPrefab;
    private uint _maxHP, _curHP;

    internal void Init(Projectile ammo, uint memberHP)
    {
        maxHP = memberHP;
        curHP = maxHP;
        ammoPrefab = ammo;
    }

    internal void LaunchProjectile(Vector2 target, Stock ammoStock)
    {
        if (ammoStock.TryTake(1) != 0)
        {
            Projectile proj = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            proj.speed = throwSpeed;
            proj.Launch(target);
        }
    }

    public void TakeDamage(uint damage)
    {
        curHP -= damage;

        if (curHP == 0)
            OnDefeated?.Invoke(gameObject);
    }
}