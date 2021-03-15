using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera mainCamera = null;
    private Transform cameraTarget = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private float rotationSpeed = 100f;

    private void Start()
    {
        GameObject cameraTar = new GameObject("Camera Target");
        cameraTarget = cameraTar.transform;

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
        FollowMainTarget();
        ControlCamera();
    }

    private void ControlCamera()
    {
        float mouseMovement = inputControl.mouseMovement;

        if (Mathf.Abs(mouseMovement) > 0.1f)
        {
            Vector3 rot = new Vector3(0f, mouseMovement * rotationSpeed * Time.deltaTime * 100, 0f);
            cameraTarget.Rotate(rot, Space.Self);
        }
    }

    private void FollowMainTarget()
    {
        cameraTarget.position = transform.position; 
    }
}
