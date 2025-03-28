using UnityEngine;

public class pillsScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControler>().addSerum();
            Destroy(gameObject);
        }
    }
}
