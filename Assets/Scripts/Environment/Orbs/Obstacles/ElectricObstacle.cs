using UnityEngine;
using Mirror;
using System.Collections;

public class ElectricObstacle : DamageObstacle
{
    [SyncVar]
    [SerializeField] float ticTimeDamage = 0.1f;

    [SyncVar]
    private float currentTime = 0;
    [Server]
    override protected void DealDamageTo(Health player)
    {
        int dmg = -this.damage;//to decrease hp

        if(currentTime > ticTimeDamage)
        {
            player.CmdUpdateHealth(dmg);
            currentTime = 0;
        }

        currentTime += Time.deltaTime;
        
    }
}
