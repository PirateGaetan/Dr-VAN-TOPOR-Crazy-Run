using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.Events;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private PlayerControler player; 
    [Header("GAME DESIGN")]
    [SerializeField] private float SerumDecreaseSpeed;
    [SerializeField] public float dammageBlueCatalyser;
    [SerializeField] public float dammageGreenCatalyser;

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
            TimerManagement();
            score +=1f;
            ApplyGamePlayTimerEffect();
        }
    }

    private void TimerManagement()
    {
        nextTimerGamePlay = Time.time + 1f;
        timerGamePlay++;
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
