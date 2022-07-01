using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private uint damage;

    internal float speed;

    internal void Launch(Vector2 target)
    {
        StartCoroutine(FlyToTarget(target));
    }

    private IEnumerator FlyToTarget(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.Translate((target - (Vector2)transform.position).normalized * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        CheckHit();
    }

    private void CheckHit()
    {
        RaycastHit2D[] membersHit;
        membersHit = Physics2D.CircleCastAll(transform.position, 0.1f, Vector2.zero, 0.1f, enemyLayer); 
        // TODO: Change enemyLayer to be set by WarMonger>WarFighter>CrewMember

        if (membersHit.Length == 0) return;

        RaycastHit2D targetHit = membersHit.Where(x => x.transform.gameObject.GetComponent<IDamageable>() != null).FirstOrDefault();

        if (targetHit.transform != null)
        {
            IDamageable damageableHit;
            if (targetHit.transform.gameObject.TryGetComponent<IDamageable>(out damageableHit))
            {
                if (damageableHit != null)
                    damageableHit.TakeDamage(damage);
            }
        }

        Destroy(gameObject, 0.1f);
    }
}