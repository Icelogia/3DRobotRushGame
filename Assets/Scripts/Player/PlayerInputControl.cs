using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    public float verticalMovement { get; private set; }
    public float rotationMovement { get; private set; }
    public bool ability { get; private set; }
    public bool charge { get; private set; }

    private void Update()
    {
        verticalMovement = Input.GetAxis("Vertical");
        rotationMovement = Input.GetAxis("Horizontal");
        ability = Input.GetKeyDown(KeyCode.Space);
    }

}
