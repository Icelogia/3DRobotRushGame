using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera mainCamera = null;
    [SerializeField] private Transform cameraTarget = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private float rotationSpeed = 100f;

    private void Start()
    {
        SetCamera();
    }

    private void SetCamera()
    {
        mainCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCamera.Follow = cameraTarget;
        mainCamera.LookAt = cameraTarget;
    }

    private void Update()
    {
        float mouseMovement = inputControl.mouseMovement;
        if (Mathf.Abs(mouseMovement) > 0.1f)
        {
            Vector3 rot = new Vector3(0f, mouseMovement * Time.deltaTime * 1000, 0f);
            cameraTarget.Rotate(rot, Space.Self);
        }
    }
}
