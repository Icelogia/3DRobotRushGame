using UnityEngine;
using Mirror;
using System.Collections;
using Cinemachine;

public class Player : NetworkBehaviour
{

    private CinemachineVirtualCamera mainCamera = null;

    [SerializeField] private Renderer playersMesh;

    [SyncVar]
    private Color playerColor;


    private void Start()
    {
        if(!hasAuthority) { return; }

        StartCoroutine("SetColor");//Waiting for all players to join game scene to set colors
        SetCamera();
    }

    [Client]
    private void SetCamera()
    {
        mainCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCamera.Follow = this.transform;
        mainCamera.LookAt = this.transform;
    }

    [Client]
    private IEnumerator SetColor()
    {
        yield return new WaitForSeconds(1);
        playerColor = ColorSetting.color;
        playersMesh.material.color = playerColor;
        CmdSetColorOnPlayer(playerColor);
    }


    [Command]
    public void CmdSetColorOnPlayer(Color color)
    {
        playersMesh.material.color = color;
        RpcSetColorOnPlayer(color);
    }

    [ClientRpc]
    public void RpcSetColorOnPlayer(Color color)
    {
        playersMesh.material.color = color;
    }



}
