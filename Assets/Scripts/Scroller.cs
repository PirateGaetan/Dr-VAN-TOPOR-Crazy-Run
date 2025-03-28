using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] public float speedChunk = 6f;
    void Update()
    {
        foreach(Transform child in transform)
        {
            child.position += Vector3.back * Time.deltaTime * speedChunk;
        }
        
    }
}
