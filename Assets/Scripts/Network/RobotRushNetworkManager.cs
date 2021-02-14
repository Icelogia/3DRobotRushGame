using UnityEngine;
using Mirror;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RobotRushNetworkManager : NetworkRoomManager
{
    private bool isGameInProgress = false;
    public Color playersColor;

    #region Server
    public override void OnServerConnect(NetworkConnection conn)
    {
        if(!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        if(newSceneName == this.GameplayScene)
        {
            isGameInProgress = true;
        }
        else
        {
            isGameInProgress = false;
        }
        base.OnServerChangeScene(newSceneName);
    }

    #endregion

}
