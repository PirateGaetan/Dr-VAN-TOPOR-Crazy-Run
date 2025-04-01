using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerControler player; 
    [SerializeField] private float SerumDecreaseSpeed = 3f; 
    private float timerGamePlay = 0f;
    private float nextTimerGamePlay = 0f;
    public float score = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        player.InitPlayer();
    }

    void Update()
    {
        player.UpdatePlayer();

        if (Time.time >= nextTimerGamePlay)
        {
            nextTimerGamePlay = Time.time + 1f;
            score +=1f;
            timerGamePlay++;
            Debug.Log("score écoulé : " + score);

            ApplyGamePlayTimerEffect();
        }
    }
    private void ApplyGamePlayTimerEffect()
    {
        player.removeSerum(SerumDecreaseSpeed);
        // Ajoute ici l'effet voulu, comme une diminution d'une ressource
    }

    private void OnGamePLaySceneLoad()
    {
        resetGamePlayTimer();
        resetScore();
    }

    private void resetScore()
    {
        score = 0f;
    }

    private void resetGamePlayTimer()
    {
        timerGamePlay = 0f;    
    }
}
