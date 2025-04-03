using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; 
    public TextMeshProUGUI textUI; 
    private float score;

    void Update()
    {
        score = gameManager.score;
        textUI.text = "Score: " + score.ToString("F2");
    }
}