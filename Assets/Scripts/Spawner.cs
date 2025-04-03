using UnityEngine;
using UnityEngine.Jobs;
using System.Collections.Generic;

public class spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private List<int> chunkWeights; 
    [SerializeField] private Transform container;
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ChunkTag"))
        {
            var chunk = other.gameObject.transform.parent.parent;
            var end = chunk.transform.Find("End");

            var newChunk = Instantiate(GetRandomChunk(), end.position, end.rotation);
            newChunk.transform.SetParent(container);
        }
    }
    // private GameObject GetRandomChunk()
    // {
    //     return chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
    // }
    private GameObject GetRandomChunk()
    {
        int totalWeight = 0;
        foreach (int weight in chunkWeights)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < chunkPrefabs.Count; i++)
        {
            cumulativeWeight += chunkWeights[i];
            if (randomValue < cumulativeWeight) return chunkPrefabs[i];
        }

        return chunkPrefabs[0];
    }
}
