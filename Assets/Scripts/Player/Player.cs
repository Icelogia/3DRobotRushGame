﻿using UnityEngine;
using Mirror;
using System.Collections;
using MainMenu;

public class Player : NetworkBehaviour
{
    

    [SerializeField] private Renderer playersMesh;

    [SyncVar]
    private Color playerColor;
    [SyncVar]
    private string nick;

    private void Start()
    {
        if(!hasAuthority) { return; }

        StartCoroutine("SetColor");//Waiting for all players to join game scene to set colors
        StartCoroutine("SetNick");
    }

    #region Nick
    [Client]
    private IEnumerator SetNick()
    {
        yield return new WaitForSeconds(1);
        nick = PlayerNameInput.Nick;
        CmdSetNick(nick);
        //Set Nick over player
    }
    [Command]
    public void CmdSetNick(string nick)
    {
        this.nick = nick;
        RpcSetNick(nick);
    }

    [ClientRpc]
    public void RpcSetNick(string nick)
    {
        this.nick = nick;
    }
    #endregion Nick

    #region Materials
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

    [Command]
    public void CmdSetEmmisionColorOnPlayer(Color color)
    {
        playersMesh.materials[1].SetColor("Color_F3EA4B39", color);
        RpcSetEmissionColorOnPlayer(color);
    }

    [ClientRpc]
    public void RpcSetEmissionColorOnPlayer(Color color)
    {
        playersMesh.materials[1].SetColor("Color_F3EA4B39", color);
    }
    #endregion Materials
}
