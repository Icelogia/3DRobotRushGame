using UnityEngine;
using Mirror;
using MainMenu;
using UnityEngine.UI;

public class RoomPlayer : NetworkRoomPlayer
{
    private RobotRushNetworkManager room;
    private RobotRushNetworkManager Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkRoomManager.singleton as RobotRushNetworkManager;
        }
    }
    [SyncVar]
    public string playerName = "Player";
    [SyncVar]
    private bool isReady = false;
    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();
        SetPlayerName();

        if (this.hasAuthority)
        {
            if (!Room.roomPlayers.Contains(this))
                Room.roomPlayers.Add(this);
        }

        var lobbyPanel = GameObject.FindGameObjectWithTag("Lobby Panel");
        if (lobbyPanel)
        {
            this.transform.SetParent(lobbyPanel.transform);
        }
        else
        {
            Debug.LogError("There is no lobby panel on scene or there is problem with lobbyPanel tag!");
        }
    }

    [Client]
    void SetPlayerName()
    {
        playerName = PlayerNameInput.Nick;
        CmdSendNameToServer(playerName);
    }

    [Command]
    void CmdSendNameToServer(string nameToSend)
    {
        RpcSetPlayerName(nameToSend);
    }

    [ClientRpc]
    void RpcSetPlayerName(string name)
    {
        gameObject.GetComponentInChildren<Text>().text = name;
    }

    [Command]
    public void CmdReadyUp()
    {
        isReady = !isReady;
        Debug.Log(isReady);
        RpcSetReady(isReady);
    }

    [ClientRpc]
    void RpcSetReady(bool isReady)
    {
        this.readyToBegin = isReady;
    }
}
