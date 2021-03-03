using UnityEngine;
using Mirror;
public class MovementObstacle : NetworkBehaviour, IObstacle
{
    [ClientRpc]
    virtual protected void ChangeMovementOf(Movement player) { }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        Movement player;

        if (other.gameObject.TryGetComponent<Movement>(out player))
        {
            ChangeMovementOf(player);
        }
    }
}
