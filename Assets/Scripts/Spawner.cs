using UnityEngine;
using UnityEngine.Jobs;
using System.Collections.Generic;

public class spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabs;
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
        return chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
    }
}
