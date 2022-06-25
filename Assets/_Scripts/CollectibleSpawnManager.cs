using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnManager : MonoBehaviour
{
    [SerializeField] private Traveler spawnOrigin;
    [SerializeField] private int maxSpawened;
    [SerializeField] private float spawnMaxRadius;
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float destroyRadius;
    [SerializeField] private float spawnInterval;
    [SerializeField] private List<Collectible> spawnables = new List<Collectible>();

    private bool spawning = false;
    private Coroutine spawner;
    private int spawned = 0;

    private void OnEnable()
    {
        spawnOrigin.OnStartMove += EnableSpawning;
        spawnOrigin.OnStopMove += DisableSpawning;
        Collectible.OnCollected += RemoveSpawned;
    }

    private void OnDisable()
    {
        spawnOrigin.OnStartMove -= EnableSpawning;
        spawnOrigin.OnStopMove -= DisableSpawning;
        Collectible.OnCollected -= RemoveSpawned;
    }

    private void RemoveSpawned()
    {
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

    private IEnumerator SpawnFromCollection(List<Collectible> spawnables, float interval)
    {
        while (spawning)
        {
            yield return new WaitForSeconds(interval);

            if ((spawned < maxSpawened))
                Spawn(spawnables[Random.Range(0, spawnables.Count)]);
            else
                DisableSpawning();
        }
    }

    private void Spawn(Collectible obj)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
        GameObject go = Instantiate(obj.gameObject, spawnOrigin.transform.position + (Vector3)spawnPos, Quaternion.identity);
        Collectible collectible = go.GetComponent<Collectible>();
        collectible.liveTarget = spawnOrigin.transform;
        collectible.liveDistance = destroyRadius;
        spawned++;
    }
}
