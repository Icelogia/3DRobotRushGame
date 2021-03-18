using UnityEngine;
using Mirror;

public class PlayerInputControl : NetworkBehaviour
{
    public float verticalMovement { get; private set; }
    public float rotationMovement { get; private set; }
    public bool ability { get; private set; }
    public bool charge { get; private set; }
    public float mouseMovement { get; private set; }

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) { return; }

        verticalMovement = Input.GetAxis("Vertical");
        rotationMovement = Input.GetAxis("Horizontal");
        ability = Input.GetKeyDown(KeyCode.Space);
        charge = Input.GetKeyDown(KeyCode.LeftShift);
        mouseMovement = Input.GetAxis("Mouse X");
    }

}
