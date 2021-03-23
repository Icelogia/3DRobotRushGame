using UnityEngine;
using Mirror;

public class Lava : NetworkBehaviour
{
    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        NetworkIdentity player = other.GetComponent<NetworkIdentity>();

        if (player != null)
        {
            Movement playerMovement = other.GetComponent<Movement>();
            playerMovement.TRpcDrown(player.connectionToClient);
        }
    }
}
