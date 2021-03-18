using UnityEngine;
using Mirror;
using System.Collections;

public class Player : NetworkBehaviour
{
    

    [SerializeField] private Renderer playersMesh;
    [SerializeField] private TrailRenderer[] trails = null;

    [SyncVar]
    private Color playerColor;


    private void Start()
    {
        if(!hasAuthority) { return; }

        StartCoroutine("SetColor");//Waiting for all players to join game scene to set colors
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

    #region Trails
    [Server]
    public void TurnTrailsOn()
    {
        ChangeTrailsState(true);
        StartCoroutine(TurnTrailsOff());

        RpcTurnTrailsOn();
    }

    [ClientRpc]
    public void RpcTurnTrailsOn()
    {
        ChangeTrailsState(true);

        StartCoroutine(TurnTrailsOff());
    }

    private void ChangeTrailsState(bool state)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].emitting = state;
        }
    }
    private IEnumerator TurnTrailsOff()
    {
        yield return new WaitForSeconds(3);

        ChangeTrailsState(false);
    }
    #endregion Trails
}
