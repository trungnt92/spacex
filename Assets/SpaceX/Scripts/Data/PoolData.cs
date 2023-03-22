using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolData", menuName = "GameData/PoolData", order = 1)]
public class PoolData : ScriptableObject
{
    public List<GameObject> prefabs;

    public GameObject GetRandomPrefab()
    {
        var idx = UnityEngine.Random.Range(0, prefabs.Count);
        return prefabs[idx];
    }
}