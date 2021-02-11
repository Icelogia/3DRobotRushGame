using UnityEngine;
using Mirror;

namespace MainMenu
{
    public class HostLobbyMenu : MonoBehaviour
    {
        public void HostGame()
        {
            RobotRushNetworkManager.singleton.StartHost();
        }
    }
}
