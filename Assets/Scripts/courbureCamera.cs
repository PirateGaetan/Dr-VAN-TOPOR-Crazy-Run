using UnityEngine;

public class CameraCurvature : MonoBehaviour
{
    [SerializeField] private float curvatureAmount = 1.5f; // Ajuste l'effet

    void Update()
    {
        Camera.main.projectionMatrix = Matrix4x4.Perspective(
            Camera.main.fieldOfView + curvatureAmount,
            Camera.main.aspect,
            Camera.main.nearClipPlane,
            Camera.main.farClipPlane
        );
    }
}