using UnityEngine;

public class purplePillsScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControler>().addPurpleSerum();
            Destroy(gameObject);
        }
    }
}

