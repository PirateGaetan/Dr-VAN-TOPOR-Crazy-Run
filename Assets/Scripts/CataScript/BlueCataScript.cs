using UnityEngine;

public class BlueCataScript : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BOOOM 1");
        if (other.CompareTag("Player"))
        {
            Debug.Log("BOOOM 2");
            other.GetComponent<PlayerControler>().removeSerum(gameManager.dammageBlueCatalyser);
            Destroy(gameObject);
        }
    }
}

