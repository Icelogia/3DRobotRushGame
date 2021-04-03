using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Net.Sockets;

namespace MainMenu
{
    public class JoinLobbyMenu : MonoBehaviour
    {
        [SerializeField] private InputField ipAddressField = null;

        public void JoinGame()
        {
            string address = ipAddressField.text;
            RobotRushNetworkManager.singleton.networkAddress = address;
            try
            {
                RobotRushNetworkManager.singleton.StartClient();
            }
            catch(SocketException)
            {
                RobotRushNetworkManager.singleton.StopClient();
            }
            
        }

    }
}
