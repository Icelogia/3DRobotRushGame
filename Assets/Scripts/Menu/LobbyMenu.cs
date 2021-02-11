using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class LobbyMenu : NetworkBehaviour
{
    public static event Action HandleChangeReady;

    public void StartGame()
    {

    }

    public void LeaveLobby()
    {
        if(isServer)
            RobotRushNetworkManager.singleton.StopHost();
        else
            RobotRushNetworkManager.singleton.StopClient();
    }

    [Client]
    public void SetReady()
    {
        HandleChangeReady?.Invoke();
    }

}
