using UnityEngine;
using Mirror;

public class ElectricObstacle : DamageObstacle
{
    [Server]
    override protected void DealDamageTo(Health player)
    {
        int dmg = -this.damage;//to decrease hp
        player.CmdUpdateHealth(dmg);
    }
}
