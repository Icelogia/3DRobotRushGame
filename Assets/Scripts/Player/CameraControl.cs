using UnityEngine;
using Cinemachine;
using Mirror;
using System.Collections.Generic;

public class CameraControl : NetworkBehaviour
{
    private CinemachineVirtualCamera mainCamera = null;
    private Transform cameraTarget = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private float rotationSpeed = 100f;

    private bool isDead = false;
    private int currentWatchedPlayer = 0;

    readonly private static List<Transform> playersTransform = new List<Transform>();
    private Transform lastCameraTarget = null;


    [ClientCallback]
    private void Start()
    {
        if(!CameraControl.playersTransform.Contains(transform))
        {
            CameraControl.playersTransform.Add(transform);
        }

        if (!hasAuthority) { return; }

        GameObject cameraTar = new GameObject("Camera Target");
        cameraTarget = cameraTar.transform;
        lastCameraTarget = GameObject.FindGameObjectWithTag("Camera Last Target").transform;

        SetCamera();
        this.GetComponent<Health>().Death += ChangeMainTargetWithDeath;
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

        if(isDead)
        {
            if(playersTransform.Count > 0)
            {
                SetCameraNextTarget();
            }
            SetCamera();
        }
        

        FollowMainTarget();
        ControlCamera();

    }

    [Client]
    private void SetCameraNextTarget()
    {
        if (inputControl.mouseLeftClick)
        {
            if (currentWatchedPlayer == 0)
            {
                currentWatchedPlayer = playersTransform.Count - 1;
            }
            else
            {
                currentWatchedPlayer -= 1;
            }
        }
        else if (inputControl.mouseRightClick)
        {
            if (currentWatchedPlayer == playersTransform.Count - 1)
            {
                currentWatchedPlayer = 0;
            }
            else
            {
                currentWatchedPlayer += 1;
            }
        }
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
        if(isDead)
            cameraTarget.position = playersTransform[currentWatchedPlayer].position; 
        else
            cameraTarget.position = transform.position;
    }

    [Client]
    private void ChangeMainTargetWithDeath()
    {
        if(playersTransform.Count <= 1) 
        {
            cameraTarget = lastCameraTarget;
            return;
        }

        foreach(Transform playerObjTrans in playersTransform)
        {
            if(playerObjTrans == this.transform)
            {
                isDead = true;
                playersTransform.Remove(playerObjTrans);
                SetCamera();
                break;
            }
        }
    }
}
