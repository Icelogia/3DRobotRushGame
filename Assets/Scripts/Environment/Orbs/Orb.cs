using UnityEngine;
using Mirror;

[RequireComponent(typeof(Collider))]
public class Orb : NetworkBehaviour
{
    [SerializeField] private GameObject effectPrefab = null;

    [SerializeField] private int hpToRegenerate = 10;

    [ClientCallback]
    private void OnTriggerEnter(Collider other)
    {
        Abilities player;

        if (other.gameObject.TryGetComponent<Abilities>(out player))
        {
            player.SetEffectPrefab(effectPrefab);

            Health health = player.GetComponent<Health>();

            health.CmdUpdateHealth(hpToRegenerate);

            NetworkServer.Destroy(this.gameObject);
        }
    }
}
