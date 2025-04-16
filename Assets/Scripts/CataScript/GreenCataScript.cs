using UnityEngine;

public class GreenCataScript : MonoBehaviour
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
            player.RemoveGreenSerum(gameManager.dammageGreenCatalyser);
            gameManager.TriggerSpeedReduction(gameManager.CataColisionDuration, gameManager.CataColisionSpeedReduction);
            Destroy(gameObject);
        }
    }
}