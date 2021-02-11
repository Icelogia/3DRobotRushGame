using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class PlayerNameInput : MonoBehaviour
    {
        [SerializeField] private InputField nameInputField = null;
        private const string playerDefaultName = "Player Name";
        [SerializeField] private Button hostButton = null;
        [SerializeField] private Button joinButton = null;

        public static string Nick { get; private set; }

        private void Start()
        {
            nameInputField.text = playerDefaultName;
        }
        public void OnNameChanged()
        {
            
            bool isNameGood = !string.IsNullOrEmpty(nameInputField.text);
            if (isNameGood)
            {
                Nick = nameInputField.text;
                hostButton.interactable = true;
                joinButton.interactable = true;
            }
            else
            {
                hostButton.interactable = false;
                joinButton.interactable = false;
            }
        }
    }
}
