using UnityEngine;
using Mirror;
using MainMenu;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        LobbyMenu.HandleChangeReady += CmdReadyUp;
    }

    private void OnDestroy()
    {
        LobbyMenu.HandleChangeReady -= CmdReadyUp;
    }

    public override void OnStopClient()
    {
        if (this.hasAuthority && SceneManager.GetActiveScene().Equals("Lobby"))
        {
            SceneManager.LoadScene("Main_Menu");
        }

        base.OnStopClient();

    }

    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();
        SetPlayerName();


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
    private void SetPlayerName()
    {
        playerName = PlayerNameInput.Nick;
        CmdSendNameToServer(playerName);
    }

    [Command]
    private void CmdSendNameToServer(string nameToSend)
    {
        RpcSetPlayerName(nameToSend);
    }

    [ClientRpc]
    private void RpcSetPlayerName(string name)
    {
        gameObject.GetComponentInChildren<Text>().text = name;
    }

    [Command]
    private void CmdReadyUp()
    {
        isReady = !isReady;
        Toggle readyToggle = this.gameObject.GetComponentInChildren<Toggle>();
        readyToggle.isOn = isReady;
        RpcSetReady(isReady);
    }

    [ClientRpc]
    private void RpcSetReady(bool isReady)
    {
        Toggle readyToggle = this.gameObject.GetComponentInChildren<Toggle>();
        readyToggle.isOn = isReady;
        this.readyToBegin = isReady;
    }
}
