using UnityEngine;
using Mirror;
using MainMenu;
using System;
using System.Collections.Generic;

public class RobotRushNetworkManager : NetworkRoomManager
{
    private bool isGameInProgress = false;

    public List<Player> players { get; } = new List<Player>();

    #region Server
    public override void OnServerConnect(NetworkConnection conn)
    {
        if(!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnStopHost()
    {
        players.Clear();

        base.OnStopServer();
    }

    public override void OnStartHost()
    {

    }
    #endregion

}
