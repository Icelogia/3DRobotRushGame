using UnityEngine;
using Mirror;

public class Abilities : NetworkBehaviour
{
    private GameObject effectPrefab = null;

    [SerializeField] private Health health = null;
    [SerializeField] private PlayerInputControl inputControl = null;

    [ClientCallback]
    private void Update()
    {
        if(!isLocalPlayer) { return; }

        if (inputControl.ability)
        {
            CmdUsingEffect();
        }
    }

    [Command]
    private void CmdUsingEffect()
    {
        if (effectPrefab)
        {
            var effect = Instantiate(effectPrefab, transform.position, transform.rotation);
            effectPrefab = null;
            NetworkServer.Spawn(effect);

            RpcUsingEffect();
        }
    }

    [ClientRpc]
    private void RpcUsingEffect()
    {
        if (effectPrefab)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
            effectPrefab = null;
        }
    }

    

    [Client]
    public void SetEffectPrefab(GameObject prefab)
    {
        if (effectPrefab == null)
            effectPrefab = prefab;
    }
  
}
