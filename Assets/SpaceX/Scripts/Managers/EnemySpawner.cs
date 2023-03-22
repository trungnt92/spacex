using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool objectPool;
    public GameObject player;

    private void Awake()
    {
        objectPool.SetUp();
    }

    public void SpawnEnemy(int numOfEnemy)
    {
        for (int i = 0; i < numOfEnemy; i++)
        {
            SpawnNextObj();
        }
    }

    void SpawnNextObj()
    {
        var enemy = objectPool.GetPooledObject();
        var controller = enemy.GetComponent<AIShipController>();
        controller.trackingTarget = player;
        enemy.transform.position = GameHelper.RandomPos(300, 600);
        enemy.SetActive(true);
    }
}
