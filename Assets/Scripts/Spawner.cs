using UnityEngine;
using UnityEngine.Jobs;
using System.Collections.Generic;

public class spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabsLevel1;
    [SerializeField] private List<int> chunkWeightsLevel1; 
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
    private GameObject GetRandomChunk()
    {
        int totalWeight = 0;
        foreach (int weight in chunkWeightsLevel1)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < chunkPrefabsLevel1.Count; i++)
        {
            cumulativeWeight += chunkWeightsLevel1[i];
            if (randomValue < cumulativeWeight) return chunkPrefabsLevel1[i];
        }

        return chunkPrefabsLevel1[0];
    }
}
