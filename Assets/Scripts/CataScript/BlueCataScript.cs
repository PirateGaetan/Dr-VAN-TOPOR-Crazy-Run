using UnityEngine;

public class BlueCataScript : MonoBehaviour
{
    private GameManager gameManager; 
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControler player = other.GetComponent<PlayerControler>();
            player.RemoveBlueSerum(gameManager.dammageBlueCatalyser);
            Destroy(gameObject);
        }
    }
}

