using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace MainMenu
{
    public class JoinLobbyMenu : MonoBehaviour
    {
        [SerializeField] private Button joinButton = null;
        [SerializeField] private InputField ipAddressField = null;

        public void JoinGame()
        {
            string address = ipAddressField.text;
            RobotRushNetworkManager.singleton.networkAddress = address;
            RobotRushNetworkManager.singleton.StartClient();

            joinButton.interactable = false;
        }

    }
}
