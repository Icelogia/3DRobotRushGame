using UnityEngine;
using Mirror;
using System;

public class Player : NetworkBehaviour
{

    [SerializeField] private Renderer playersMesh;
    [SyncVar]
    [SerializeField] private Color playerColor;

    [ClientCallback]
    private void Start()
    {
        var networkManager = FindObjectOfType<RobotRushNetworkManager>();
        playerColor = networkManager.playersColor;
        playersMesh.material.color = playerColor;
        CmdSetColorOnPlayer(playerColor);
    }


    [Command]
    public void CmdSetColorOnPlayer(Color color)
    {
        if (isLocalPlayer)
            playersMesh.material.color = color;
        RpcSetColorOnPlayer(color);
    }

    [ClientRpc]
    public void RpcSetColorOnPlayer(Color color)
    {
            playersMesh.material.color = color;
    }



}
