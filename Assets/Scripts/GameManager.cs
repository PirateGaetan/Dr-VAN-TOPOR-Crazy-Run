using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerControler player; 
    [Header("Timer Settings")]
    [SerializeField] private float timerDifficulty1 = 1f; 
    private float timerGamePlay = 0f;
    private float nextTimerGamePlay = 0f;

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
            nextTimerGamePlay = Time.time + 1f; // Ajoute 1 seconde au prochain déclenchement
            timerGamePlay++;
            Debug.Log("Temps écoulé : " + timerGamePlay);

            ApplyGamePlayTimerEffect();
        }
    }
    private void ApplyGamePlayTimerEffect()
    {
        player.removeSerum(timerDifficulty1);
        // Ajoute ici l'effet voulu, comme une diminution d'une ressource
    }

    private void OnGamePLaySceneLoad()
    {
        resetGamePlayTimer();

    }

    private void resetGamePlayTimer()
    {
        timerGamePlay = 0f;    
    }
}
