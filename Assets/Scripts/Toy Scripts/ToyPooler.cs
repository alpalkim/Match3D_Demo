using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPooler : MonoBehaviour
{
    [SerializeField] List<ToyPair> wholePrefabs;
    public Dictionary<int,ToyPair> WholeToyPairs { get; set; }

    private void Awake()
    {
        WholeToyPairs = GenerateToyDictionary(wholePrefabs);
    }

    public Dictionary<int, ToyPair> GenerateToyDictionary(List<ToyPair> prefabs)
    {
        if(prefabs.Count <= 0 || prefabs == null) { return null; }

        Dictionary<int, ToyPair> pool = new Dictionary<int, ToyPair>();

        int index = 0;
        foreach (ToyPair pair in prefabs)
        {
            int pairHash = index;
            if (pool.ContainsKey(pairHash))
            {
                continue;
            }
            pool.Add(pairHash, pair);
            index++;
        }

        return pool;
    }

}