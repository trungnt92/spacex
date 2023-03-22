using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public PoolData poolData;

    private List<GameObject> mPooledObjects;
    public int poolingSize;
    public GameObject parentGO;

    public void SetUp()
    {
        mPooledObjects = new List<GameObject>();
        for (int i = 0; i < poolingSize; i++)
        {
            var newObj = Instantiate(poolData.GetRandomPrefab(), parentGO.transform);
            newObj.SetActive(false);
            mPooledObjects.Add(newObj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolingSize; i++)
        {
            if (!mPooledObjects[i].activeInHierarchy)
            {
                return mPooledObjects[i];
            }
        }
        return null;
    }

    public void DeactiveAll()
    {
        for (int i = 0; i < poolingSize; i++)
        {
            mPooledObjects[i].SetActive(false);
        }
    }
}

