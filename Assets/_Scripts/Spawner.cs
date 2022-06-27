using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] protected Traveler spawnOrigin;
    [SerializeField] protected int maxSpawened;
    [SerializeField] protected float spawnMaxRadius;
    [SerializeField] protected float spawnMinRadius;
    [SerializeField] protected float destroyRadius;
    [SerializeField] protected float spawnInterval;
    [SerializeField] protected List<T> spawnables = new List<T>();

    protected bool spawning = false;
    protected Coroutine spawner;
    protected int spawned = 0;

    private void OnEnable()
    {
        spawnOrigin.OnStartMove += EnableSpawning;
        spawnOrigin.OnStopMove += DisableSpawning;
    }

    private void OnDisable()
    {
        spawnOrigin.OnStartMove -= EnableSpawning;
        spawnOrigin.OnStopMove -= DisableSpawning;
    }

    private void AddSpawned(ISpawnable obj)
    {
        print($"Object of type {obj.GetType()} has been spawned");
        obj.OnDestroyed += RemoveSpawned;
        spawned++;
    }

    private void RemoveSpawned(ISpawnable obj)
    {
        print($"Object of type {obj.GetType()} has been removed");
        obj.OnDestroyed -= RemoveSpawned;
        spawned--;
    }

    private void EnableSpawning()
    {
        spawning = true;
        if (spawner != null)
            spawner = null;

        spawner = StartCoroutine(SpawnFromCollection(spawnables, spawnInterval));
    }

    private void DisableSpawning()
    {
        spawning = false;
    }

    private IEnumerator SpawnFromCollection(List<T> spawnables, float interval)
    {
        while (spawning)
        {
            yield return new WaitForSeconds(interval);

            if ((spawned < maxSpawened))
            {
                ISpawnable spawn = Spawn(spawnables[Random.Range(0, spawnables.Count)]).GetComponent<ISpawnable>();
            }
            else
            {
                DisableSpawning();
            }
        }
    }

    protected virtual GameObject Spawn(T obj)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
        GameObject go = Instantiate(obj.gameObject, spawnOrigin.transform.position + (Vector3)spawnPos, Quaternion.identity);
        ISpawnable spawn = go.GetComponent<ISpawnable>();
        AddSpawned(spawn);
        return go;
    }
}
