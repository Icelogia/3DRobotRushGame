using UnityEngine;
using Mirror;

public class OilObstacle : MovementObstacle
{
    [SyncVar]
    [SerializeField] private float additionalForce = 1;
    [SyncVar]
    private int rotationRightMultiplier;

    private void Start()
    {
        rotationRightMultiplier = Random.Range(0, 2);
    }

    [Server]
    override protected void ChangeMovementOf(Movement player)
    {
        player.RpcAddForce(additionalForce);

        int rotMulti = rotationRightMultiplier == 1 ? 1 : -1;
        player.RpcRotate(rotMulti);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        PlayerEffectsController player = other.GetComponent<PlayerEffectsController>();
        player.TurnTrailsOn();
    }
}
