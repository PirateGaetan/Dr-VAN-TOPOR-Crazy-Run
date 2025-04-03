using Unity.Cinemachine;
using UnityEngine;

public class cameraNoise : MonoBehaviour
{
    public static cameraNoise Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineCam;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        var noise = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                var noise = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noise.AmplitudeGain = 0f; // Arrête la secousse
            }
        }
    }
}
