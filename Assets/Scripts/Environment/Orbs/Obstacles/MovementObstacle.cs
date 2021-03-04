using UnityEngine;
using Mirror;
public class MovementObstacle : NetworkBehaviour, IObstacle
{
    [Server]
    virtual protected void ChangeMovementOf(Movement player) { }

    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        Movement player;

        if (other.gameObject.TryGetComponent<Movement>(out player))
        {
            ChangeMovementOf(player);
        }
    }
}
