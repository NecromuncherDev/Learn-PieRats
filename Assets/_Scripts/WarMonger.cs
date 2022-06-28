using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieRatShip), typeof(Collider2D))]
public class WarMonger : MonoBehaviour
{
    public static event Action<WarMonger, WarMonger> OnWarDeclared;
    public static event Action<WarMonger, WarMonger> OnWarDenied;
    public static event Action<WarMonger, WarMonger> OnWarAccepted;

    [SerializeField] private float aggressionRange;
    [SerializeField] private LayerMask aggressionLayer;
    [SerializeField] private bool aggressive = false;

    private RaycastHit2D enemyTarget;
    private WarMonger enemy;

    private void Update()
    {
        if (aggressive && enemy == null)
        {
            if (enemyTarget.collider == null)
                enemyTarget = Physics2D.CircleCast(transform.position, aggressionRange, Vector2.zero, 0f, aggressionLayer);
            else
                DeclareWar(enemyTarget.collider.gameObject);
        }
    }

    private void DeclareWar(GameObject target)
    {
        print($"{this.gameObject} is trying to declare war!");

        WarMonger targetEnemy;

        if (target.TryGetComponent(out targetEnemy))
        {
            OnWarDeclared?.Invoke(this, targetEnemy);

            print($"{this.gameObject} has declared war on {targetEnemy.gameObject}!");

            if (targetEnemy.ConsiderWar(this))
            {
                OnWarAccepted?.Invoke(this, targetEnemy);
                print($"{targetEnemy.gameObject} has accepted {this.gameObject}'s challenge!");
                enemy = targetEnemy;
            }
        }
    }

    internal bool ConsiderWar(WarMonger target)
    {
        if (enemy == null)
        {
            enemy = target;
            return true;
        }

        return false;
    }
}
