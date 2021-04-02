using UnityEngine;
using Mirror;

[RequireComponent(typeof(Collider))]
public class Orb : OrbElement
{
    [SerializeField] private GameObject effectPrefab = null;
    [SerializeField] private Color lightColor;

    [SerializeField] private int hpToRegenerate = 10;

    [ClientCallback]
    private void OnTriggerEnter(Collider other)
    {
        Abilities player;

        if (other.gameObject.TryGetComponent<Abilities>(out player))
        {
            player.SetEffectPrefab(effectPrefab, lightColor);

            Health health = player.GetComponent<Health>();

            health.CmdUpdateHealth(hpToRegenerate);

            NetworkServer.Destroy(this.gameObject);
        }
    }
}
