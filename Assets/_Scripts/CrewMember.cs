using System;
using System.Collections;
using UnityEngine;

public class CrewMember : MonoBehaviour, IDamageable
{
    public event Action<IDamageable> OnDefeated;
    public uint maxHP { get => maxHP; set => maxHP = value; }
    public uint curHP { get => curHP; set => curHP = (uint)Mathf.Min(value, maxHP); }

    [SerializeField] private float attackInterval;

    private Projectile ammoPrefab;
    private Vector2 target;
    private bool attacking = false;
    private Coroutine attackCoroutine;

    internal void Init(Projectile ammo)
    {
        curHP = maxHP;
        ammoPrefab = ammo;
    }

    internal void StartAttacking(Vector2 target, Stock ammoStock)
    {
        attacking = true;

        if (attackCoroutine != null)
            attackCoroutine = null;

        attackCoroutine = StartCoroutine(Barrage(target, ammoStock));
    }

    internal void StopAttacking()
    {
        attacking = false;
        StopCoroutine(attackCoroutine);
    }

    private IEnumerator Barrage(Vector2 target, Stock ammoStock)
    {
        while (attacking)
        {
            LaunchProjectile(target, ammoStock);
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void LaunchProjectile(Vector2 target, Stock ammoStock)
    {
        if (ammoStock.TryTake(1) != 0)
        {
            Projectile proj = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            proj.Launch(target);
        }
    }

    public void TakeDamage(uint damage)
    {
        curHP -= damage;

        if (curHP == 0)
            OnDefeated?.Invoke(this);
    }
}