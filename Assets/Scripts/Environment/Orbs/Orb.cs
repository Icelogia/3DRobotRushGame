using UnityEngine;
using Mirror;

[RequireComponent(typeof(Collider))]
public class Orb : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private GameObject effectPrefab = null;

    [SerializeField] private int hpToRegenerate = 10;

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        Abilities player;

        if (other.gameObject.TryGetComponent<Abilities>(out player))
        {
            Debug.Log(2);
            player.SetEffectPrefab(effectPrefab);

            Health health = player.GetComponent<Health>();

            health.IncreaseHealth(hpToRegenerate);

            NetworkServer.Destroy(this.gameObject);
        }

    }
}
