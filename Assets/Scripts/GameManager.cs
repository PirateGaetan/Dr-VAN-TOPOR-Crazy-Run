using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private PlayerControler player;

    [Header("GAME DESIGN")]
    [SerializeField] private float SerumDecreaseSpeed = 1f;
    [SerializeField] public float dammageBlueCatalyser = 10f;
    [SerializeField] public float dammageGreenCatalyser = 10f;
    [SerializeField] public float speedChunk = 6f;
    [SerializeField] public float increaseSpeedCHunkFactor = 1.01f;
    [SerializeField] public float timeToLevel2;
    [SerializeField] public float timeToLevel3;

    private float initialChunkSpeed;
    private float maxChunkSpeed;

    private float timerGamePlay = 0f;
    private float nextTimerGamePlay = 0f;
    private float lastSpeedIncreaseTime = -1f;

    public float score = 0f;

    public enum GameLevel { Level1, Level2, Level3 }
    private GameLevel currentLevel = GameLevel.Level1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player.InitPlayer();
        initialChunkSpeed = speedChunk;
        maxChunkSpeed = initialChunkSpeed * 3f;
    }

    void Update()
    {
        player.UpdatePlayer();

        if (Time.time >= nextTimerGamePlay)
        {
            TimerManagement();
            CheckLevelProgression();
            score += 1f;

            ApplyGamePlayTimerEffect();

            if (timerGamePlay % 3 == 0 && timerGamePlay != lastSpeedIncreaseTime)
            {
                IncreaseChunkSpeed();
                lastSpeedIncreaseTime = timerGamePlay;
            }
        }
    }

    private void TimerManagement()
    {
        nextTimerGamePlay = Time.time + 1f;
        timerGamePlay++;
    }

    private void CheckLevelProgression()
    {
        if (timerGamePlay >= timeToLevel3 && currentLevel != GameLevel.Level3)
        {
            SetLevel(GameLevel.Level3);
        }
        else if (timerGamePlay >= timeToLevel2 && currentLevel != GameLevel.Level2)
        {
            SetLevel(GameLevel.Level2);
        }
    }

    private void SetLevel(GameLevel newLevel)
    {
        currentLevel = newLevel;

        switch (currentLevel)
        {
            case GameLevel.Level1:
                player.SetLanesForLevel(1);
                break;
            case GameLevel.Level2:
                player.SetLanesForLevel(2);
                break;
            case GameLevel.Level3:
                player.SetLanesForLevel(3);
                break;
        }
    }

    private void IncreaseChunkSpeed()
    {
        speedChunk *= increaseSpeedCHunkFactor;
        if (speedChunk > maxChunkSpeed)
        {
            speedChunk = maxChunkSpeed;
        }
    }

    private void ApplyGamePlayTimerEffect()
    {
        player.removeGreenSerum(SerumDecreaseSpeed);
        player.removeBlueSerum(SerumDecreaseSpeed);
    }

    private void OnGamePLaySceneLoad()
    {
        ResetGamePlayTimer();
        ResetScore();
    }

    private void ResetScore()
    {
        score = 0f;
    }

    private void ResetGamePlayTimer()
    {
        timerGamePlay = 0f;
    }

    public float GetCurrentChunkSpeed()
    {
        return speedChunk;
    }

    public GameLevel GetCurrentLevel()
    {
        return currentLevel;
    }
}