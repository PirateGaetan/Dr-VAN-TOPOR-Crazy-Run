using UnityEngine;

public class greenPillsScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControler>().addGreenSerum();
            Destroy(gameObject);
        }
    }
}
