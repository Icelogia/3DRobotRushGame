using UnityEngine;
using Mirror;

public class DamageObstacle : NetworkBehaviour
{
    [SerializeField] protected int damage = 5;

    [Server]
    virtual protected void DealDamageTo(Health player){}
    

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        Health player;

        if (other.gameObject.TryGetComponent<Health>(out player))
        {
            DealDamageTo(player);
        }
    }

}
