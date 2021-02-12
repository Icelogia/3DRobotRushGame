using UnityEngine.SceneManagement;
using Mirror;
using System;

namespace MainMenu
{
    public class LobbyMenu : NetworkBehaviour
    {
        public static event Action HandleChangeReady;

        public void StartGame()
        {
            RobotRushNetworkManager.singleton.ServerChangeScene("Game_Scene_01");
        }

        public void LeaveLobby()
        {
            if (isServer)
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
}
