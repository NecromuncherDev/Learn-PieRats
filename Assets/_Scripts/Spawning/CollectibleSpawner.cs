using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : Spawner<Collectible>
{
    private void Start()
    {
        print("Started CollectibleSpawner");
    }

    protected override GameObject Spawn(Collectible obj)
    {
        GameObject go = base.Spawn(obj);
        Collectible collectible = go.GetComponent<Collectible>();
        collectible.liveDistance = destroyRadius;
        collectible.liveTarget = spawnOrigin.transform;

        return go;
    }
}
