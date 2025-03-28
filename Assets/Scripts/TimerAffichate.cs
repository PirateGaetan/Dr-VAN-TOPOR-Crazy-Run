using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    private Text timerText; 

    private void Start()
    {
        timerText = GetComponent<Text>();
    }

    private void Update()
    {    
        float elapsedTime = GameManager.instance.GetElapsedTime(); 
        UpdateTimerUI(elapsedTime);
        
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}"; 
    }
}
