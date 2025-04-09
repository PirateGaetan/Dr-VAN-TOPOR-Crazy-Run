using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerControler : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] Transform aimTarget;
    [SerializeField] Transform playerModel;

    [Header("MOVEMENTS")]
    [SerializeField] private float moveDuration = 1f;

    [Header("AIM")]
    [SerializeField] private float aimSpeed = 0.5f;
    [SerializeField] private float aimeRange = 5f;

    [Header("NOISE")]
    [SerializeField] private float noiseAmplitude = 0.05f;
    [SerializeField] private float noiseSpeed = 0.5f;

    [Header("Slider Serum")]
    [SerializeField] private BlueSliderManager BlueSerum;
    [SerializeField] private GreenSliderManager GreenSerum;
    [SerializeField] private PurpleSliderManager PurpleSerum;
    [SerializeField] private YellowSliderManager YellowSerum;

    [Header("EVENTS")]
    public UnityEvent<float> OnInitSlider;
    public UnityEvent<float> OnBlueSerumCollision;
    public UnityEvent<float> OnGreenSerumCollision;
    public UnityEvent<float> OnPurpleSerumCollision;
    public UnityEvent<float> OnYellowSerumCollision;

    private Vector3 targetPosition;
    private bool isMoving = false;

    // SERUM
    private float serumBlue = 100f;
    private float serumGreen = 100f;
    private float serumPurple = 100f;
    private float serumYellow = 100f;


    // LANES
    private float[] lanePositions;
    private int currentLaneIndex = 0;
    private float lastLeftTapTime = -1f;
    private float lastRightTapTime = -1f;
    private float doubleTapThreshold = 0.3f; // 300 ms

    public void InitPlayer()
    {
        targetPosition = transform.position;
        aimTarget.position = Vector3.forward * aimeRange;
        aimTarget.parent = null;

        SetLanesForLevel(1); // Niveau de départ

        OnInitSlider.Invoke(serumBlue);
        OnInitSlider.Invoke(serumGreen);
    }

    public void UpdatePlayer()
    {
        PlayerInputManagement();
        ApplyPerlinNoise();
        MaxSerumManagement();
        GameOverCheck();
    }

    #region MOVEMENT
    private void PlayerInputManagement()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
        }
    }

    private void MoveLeft()
    {
        if (currentLaneIndex > 0)
        {
            currentLaneIndex--;
            MoveToLane();
        }
    }

    private void MoveRight()
    {
        if (currentLaneIndex < lanePositions.Length - 1)
        {
            currentLaneIndex++;
            MoveToLane();
        }
    }

    private void MoveToLane()
    {
        isMoving = true;
        targetPosition = new Vector3(lanePositions[currentLaneIndex], transform.position.y, transform.position.z);
        Aim(targetPosition);
        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutSine).OnComplete(() => isMoving = false);
    }

    private void Aim(Vector3 direction)
    {
        Vector3 target = new Vector3(direction.x, 0, aimeRange);
        aimTarget.DOMove(target, aimSpeed).SetEase(Ease.InOutSine);
    }

    private void ApplyPerlinNoise()
    {
        float perlin = Mathf.PerlinNoise(Time.time * noiseSpeed, 0);
        float offset = (perlin - 0.5f) * 2f * noiseAmplitude;
        playerModel.localPosition = new Vector3(offset, 0, offset);
    }
    #endregion

    #region LANES & LEVEL
    public void SetLanesForLevel(int level)
    {
        switch (level)
        {
            case 1:
                lanePositions = new float[] { -3f, 0f, 3f };
                break;
            case 2:
                lanePositions = new float[] { -6f, -3f, 0f, 3f };
                break;
            case 3:
                lanePositions = new float[] { -6f, -3f, 0f, 3f, 6f };
                break;
            default:
                lanePositions = new float[] { 0f };
                break;
        }

        float currentX = transform.position.x;
        currentLaneIndex = GetClosestLaneIndex(currentX);
        targetPosition = new Vector3(lanePositions[currentLaneIndex], transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    private int GetClosestLaneIndex(float x)
    {
        int closest = 0;
        float minDist = Mathf.Abs(x - lanePositions[0]);
        for (int i = 1; i < lanePositions.Length; i++)
        {
            float dist = Mathf.Abs(x - lanePositions[i]);
            if (dist < minDist)
            {
                minDist = dist;
                closest = i;
            }
        }
        return closest;
    }
    #endregion

    #region SERUM
    public void addBlueSerum()
    {
        if (serumBlue < 100)
        {
            serumBlue += 10;
            OnBlueSerumCollision.Invoke(serumBlue);
        }
    }
    public void addGreenSerum()
    {
        if (serumGreen < 100)
        {
            serumGreen += 10;
            OnGreenSerumCollision.Invoke(serumGreen);
        }
    }
    public void addPurpleSerum()
    {
        if (serumPurple < 100)
        {
            serumPurple += 10;
            OnPurpleSerumCollision.Invoke(serumGreen);
        }
    }
    public void addYellowSerum()
    {
        if (serumYellow < 100)
        {
            serumYellow += 10;
            OnYellowSerumCollision.Invoke(serumGreen);
        }
    }

    public void removeBlueSerum(float minusSerum)
    {
        serumBlue -= minusSerum;
        BlueSerum.SetBlueSlider(serumBlue);
    }

    public void removeGreenSerum(float minusSerum)
    {
        serumGreen -= minusSerum;
        GreenSerum.SetGreenSlider(serumGreen);
    }
    public void removePurpleSerum(float minusSerum)
    {
        serumGreen -= minusSerum;
        PurpleSerum.SetPurpleSlider(serumGreen);
    }
    public void removeYellowSerum(float minusSerum)
    {
        serumGreen -= minusSerum;
        YellowSerum.SetYellowSlider(serumGreen);
    }

    private void MaxSerumManagement()
    {
        if (serumBlue > 100) serumBlue = 100;
        if (serumGreen > 100) serumGreen = 100;
    }
    #endregion

    #region GAME OVER
    private void GameOverCheck()
    {
        if (serumBlue <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}