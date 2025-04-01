using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [Header("REFENCES")]
    [SerializeField] Transform aimTarget;
    [SerializeField] Transform playerModel;
    [Header("MOVEMENTS")]
    [SerializeField, Range(1f, 5f)] private float range= 1f;
    [SerializeField] private float moveDuration = 1f;
    [Header("AIM")]
    [SerializeField] private float aimSpeed = 0.5f;
    [SerializeField] private float aimeRange = 5f;
    [Header("NOISE")]
    [SerializeField] private float noiseAmplitude = 0.05f;
    [SerializeField] private float noiseSpeed = 0.5f;

    [Header("EVENT")]
  
    public UnityEvent<float> OnInitSlider;
    public UnityEvent<float> OnBlueSerumCollision;

    Vector3 targetPosition;

    bool isMoving = false;
    private float serumBlue = 80f;
    private float serumGreen = 100f;
    private float serumUYellow = 100f;
    private float serumUPurple = 100f;
    public void InitPlayer()
    {
        targetPosition = transform.position;
        aimTarget.position = targetPosition;
        aimTarget.position = Vector3.forward* aimeRange;
        aimTarget.parent = null;

        OnInitSlider.Invoke(serumBlue);
    }
    public void UpdatePlayer()
    {
                if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
        }

        ApplyPerlinNoise();
        GameOverCheck();
        Debug.Log("BlueSerum : " + serumBlue);
        
    }

    #region M0UVE ACTIONS
    private void MoveLeft()
    {
        if (targetPosition.x > -range)
        {
            targetPosition += Vector3.left * range;
            MoveToTarget();
        }
    }

    private void MoveRight()
    {
        if (targetPosition.x < range)
        {
            targetPosition += Vector3.right * range;
            MoveToTarget();
        }
    }
    private void MoveToTarget()
    {
        isMoving = true;
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
        float pelinValue = Mathf.PerlinNoise(Time.time * noiseSpeed, 0);
        float offset = (pelinValue - 0.5f) * 2f * noiseAmplitude;

        playerModel.localPosition = new Vector3(offset, 0, offset);
    }
    #endregion
    #region SERUM MANAGEMENT
    public void addSerum()
    {
        if (serumBlue >= 100) return;
        else 
        {
            serumBlue +=10;
            OnBlueSerumCollision.Invoke(serumBlue);
        }
    
    }
    public void removeSerum(float minusSerum)
    {
        serumBlue = serumBlue - minusSerum;
    }

    private void GameOverCheck()
    {
        if (serumBlue <= 0) GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}

