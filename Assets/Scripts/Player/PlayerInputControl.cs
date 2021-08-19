using UnityEngine;
using Mirror;

public class PlayerInputControl : NetworkBehaviour
{
    public float verticalMovement { get; private set; }
    public float horizontalMovement { get; private set; }
    public bool ability { get; private set; }
    public bool charge { get; private set; }
    public float mouseMovement { get; private set; }
    public bool mouseRightClick { get; private set; }
    public bool mouseLeftClick { get; private set; }

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) { return; }

        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");
        ability = Input.GetKeyDown(KeyCode.Space);
        charge = Input.GetKeyDown(KeyCode.LeftShift);
        mouseMovement = Input.GetAxis("Mouse X");
        mouseLeftClick = Input.GetMouseButtonDown(0);
        mouseRightClick = Input.GetMouseButtonDown(1);
    }

}
