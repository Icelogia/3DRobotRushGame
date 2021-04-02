using UnityEngine;
using Mirror;
public class MovementObstacle : OrbElement
{
    [Server]
    virtual protected void ChangeMovementOf(Movement player) { }

    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        ChangePlayerMovement(other);
    }

    [Server]
    protected void ChangePlayerMovement(Collider other)
    {
        Movement player;

        if (other.gameObject.TryGetComponent<Movement>(out player))
        {
            ChangeMovementOf(player);
        }
    }
}
