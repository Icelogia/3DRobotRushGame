using UnityEngine;
using Mirror;
using System.Collections;

public class Player : NetworkBehaviour
{

    [SerializeField] private Renderer playersMesh;

    [SyncVar]
    private Color playerColor;


    private void Start()
    {
        if(!hasAuthority) { return; }
        StartCoroutine("SetColor");//Waiting for all players to join game scene
        
    }

    [Client]
    private IEnumerator SetColor()
    {
        yield return new WaitForSeconds(3);
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
        Debug.Log(color);
        playersMesh.material.color = color;
    }



}
