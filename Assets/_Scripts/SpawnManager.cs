using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnOrigin;
    [SerializeField] private int maxSpawened;
    [SerializeField] private float spawnMaxRadius;
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float spawnInterval;
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();

    private bool spawning = true;
    private Coroutine spawner;
    private Queue<Transform> spawned = new Queue<Transform>();

    private void Start()
    {
        spawner = StartCoroutine(SpawnFromCollection(spawnables, spawnInterval));
    }


    private IEnumerator SpawnFromCollection(List<GameObject> spawnables, float interval)
    {
        while (spawning)
        {
            if ((spawned.Count < maxSpawened))
                Spawn(spawnables[Random.Range(0, spawnables.Count)]);
            else
                Relocate(spawned.Dequeue());

            yield return new WaitForSeconds(interval);
        }
    }

    private void Spawn(GameObject obj)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
        GameObject go = Instantiate(obj, spawnOrigin.position + (Vector3)spawnPos, Quaternion.identity);
        spawned.Enqueue(go.transform);
    }

    private void Relocate(Transform obj)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
        obj.position = spawnPos;
        spawned.Enqueue(obj);
    }
}
