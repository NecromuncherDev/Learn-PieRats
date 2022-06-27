using System;
using System.Collections;
using UnityEngine;

public class RoamTraveler : Traveler, ISpawnable
{
    public event Action<ISpawnable> OnSpawned;
    public event Action<ISpawnable> OnDestroyed;

    [SerializeField] private float roamDuration;

    private Coroutine roaming;


    private void Start()
    {
        roaming = StartCoroutine(Roam());
    }

    private IEnumerator Roam()
    {
        do {
            StartMove(GetRandomMoveDir());

            yield return new WaitForSeconds(roamDuration);
        } while (moveDir != Vector2.zero);
    }

    protected override void StopMove(Vector2 direction)
    {
        StopCoroutine(roaming);
        base.StopMove(direction);
    }

    private Vector2 GetRandomMoveDir()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), 
                           UnityEngine.Random.Range(-1f, 1f));
    }
}
