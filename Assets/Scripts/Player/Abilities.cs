using UnityEngine;
using Mirror;

public class Abilities : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private GameObject effectPrefab = null;

    [SerializeField] private Health health = null;

    [Server]
    public void SetEffectPrefab(GameObject prefab)
    {
        if (effectPrefab == null)
            effectPrefab = prefab;
    }
  
}
