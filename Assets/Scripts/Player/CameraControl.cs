using UnityEngine;
using Cinemachine;
using Mirror;

public class CameraControl : NetworkBehaviour
{
    [SerializeField]private CinemachineVirtualCamera mainCamera = null;
    private Transform cameraTarget = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private float rotationSpeed = 100f;

    [ClientCallback]
    private void Start()
    {
        if(!hasAuthority) { return; }

        GameObject cameraTar = new GameObject("Camera Target");
        cameraTarget = cameraTar.transform;

        SetCamera();
    }

    [Client]
    private void SetCamera()
    {
        mainCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCamera.Follow = cameraTarget;
        mainCamera.LookAt = cameraTarget;
    }

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return; }

        FollowMainTarget();
        ControlCamera();
    }

    [Client]
    private void ControlCamera()
    {
        float mouseMovement = inputControl.mouseMovement;

        if (Mathf.Abs(mouseMovement) > 0.1f)
        {
            Vector3 rot = new Vector3(0f, mouseMovement * rotationSpeed * Time.deltaTime * 100, 0f);
            cameraTarget.Rotate(rot, Space.Self);
        }
    }

    [Client]
    private void FollowMainTarget()
    {
        cameraTarget.position = transform.position; 
    }
}
