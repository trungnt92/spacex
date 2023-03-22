using UnityEngine;
using System.Collections;

public class MinerSpawner : MonoBehaviour
{
	public ObjectPool objectPool;
    
    private void Awake()
    {
        objectPool.SetUp();
    }

    public void SpawnMiner(int numOfMiner)
    {
        for (int i = 0; i < numOfMiner; i++)
        {
            SpawnNextObj();
        }
    }

    void SpawnNextObj()
    {
        var spawnObj = objectPool.GetPooledObject();
        var collider = spawnObj.GetComponentInChildren<BoxCollider>();
        //spawnObj.transform.position = GameHelper.RandomPos();
        spawnObj.transform.localScale = Vector3.one * Random.Range(3f, 5f);
        spawnObj.transform.position = GameHelper.RandomPos(200, 600);
        spawnObj.transform.rotation = Random.rotation;

        var collided = Physics.CheckBox(collider.center, collider.bounds.extents, spawnObj.transform.rotation, LayerMask.NameToLayer("Obstacles"));
        while (collided)
        {
            spawnObj.transform.position = GameHelper.RandomPos(200, 800);
            collided = Physics.CheckBox(collider.center, collider.bounds.extents, spawnObj.transform.rotation, LayerMask.NameToLayer("Obstacles"));
        }      
        
        spawnObj.SetActive(true);
    }
}

