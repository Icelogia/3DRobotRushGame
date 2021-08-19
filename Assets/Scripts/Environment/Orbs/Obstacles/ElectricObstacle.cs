using UnityEngine;
using Mirror;

public class ElectricObstacle : DamageObstacle
{
    [SyncVar]
    [SerializeField] private float knockbackPower = 20.0f;

    [SerializeField] private Transform trans = null;

    [Server]
    override protected void DealDamageTo(Health player)
    {
        player.CmdUpdateHealth(-this.damage);
        Vector3 dir = (player.transform.position - trans.position).normalized;
        player.GetComponent<Movement>().Knockback(dir ,knockbackPower);
    }
}
