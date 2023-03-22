using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public ObjectPool objectPool;

    private void Awake()
    {
        objectPool.SetUp();
    }

    public void SpawnAsteroid(int numOfAsteroid)
    {
        for (int i = 0; i < numOfAsteroid; i++)
        {
            SpawnNextObj();
        }
    }

    void SpawnNextObj()
    {
        var spawnObj = objectPool.GetPooledObject();
        //spawnObj.transform.position = GameHelper.RandomPos();
        spawnObj.transform.position = GameHelper.RandomPos(GameHelper.NoSpawnRegion);
        spawnObj.transform.localScale = Vector3.one * Random.Range(3f, 9f);
        spawnObj.transform.rotation = Random.rotation;
        spawnObj.SetActive(true);
    }

}
