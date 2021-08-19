using UnityEngine;
using Mirror;
using System.Collections;
using MainMenu;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private Text nicknameObj = null;
    [SerializeField] private Renderer playersMesh;
    [SerializeField] private Renderer[] emmisionMeshes;

    [SyncVar]
    private Color playerColor;
    [SyncVar]
    private string nick;

    private void Start()
    {
        if(!hasAuthority) { return; }

        StartCoroutine("SetColor");//Waiting for all players to join game scene to set colors
        StartCoroutine("SetNick");

        nicknameObj.color = Color.green;
    }

    #region Nick
    [Client]
    private IEnumerator SetNick()
    {
        yield return new WaitForSeconds(1);
        nick = PlayerNameInput.Nick;
        CmdSetNick(nick);
        nicknameObj.text = nick;
    }
    [Command]
    public void CmdSetNick(string nick)
    {
        this.nick = nick;

        RpcSetNick(nick);

        Ranking ranking = FindObjectOfType<Ranking>();
        ranking.ServerAddToPlayerNamesList(nick);
        nicknameObj.text = nick;
    }

    [ClientRpc]
    public void RpcSetNick(string nick)
    {
        this.nick = nick;
        nicknameObj.text = nick;
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
        for(int i = 0; i < emmisionMeshes.Length; i++)
        {
            emmisionMeshes[i].materials[0].SetColor("Color_F3EA4B39", color);
        }
    
        RpcSetEmissionColorOnPlayer(color);
    }

    [ClientRpc]
    public void RpcSetEmissionColorOnPlayer(Color color)
    {
        for (int i = 0; i < emmisionMeshes.Length; i++)
        {
            emmisionMeshes[i].materials[0].SetColor("Color_F3EA4B39", color);
        }
    }
    #endregion Materials

    public override void OnStopServer()
    {
        //Ranking ranking = FindObjectOfType<Ranking>();
        //ranking.ServerRemoveFromPlayerNamesList(nick);
        base.OnStopServer();
    }
}
