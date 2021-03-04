using UnityEngine;
using Mirror;

public class SlimeObstacle : MovementObstacle
{
    [SyncVar]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float slowDownForce = 0.9f;

    [Server]
    override protected void ChangeMovementOf(Movement player)
    {
        player.SlowDown(slowDownForce);
    }
}
