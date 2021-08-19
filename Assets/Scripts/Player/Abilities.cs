using UnityEngine;
using Mirror;
using UnityEngine.VFX;

public class Abilities : NetworkBehaviour
{
    [SerializeField]
    private GameObject effectPrefab = null;

    [SerializeField] private Player player = null;
    [SerializeField] private Health health = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private AnimationController animController = null;
    [SerializeField] private VisualEffect electricVfx = null;

    [ClientCallback]
    private void Update()
    {
        if(!isLocalPlayer) { return; }

        if (inputControl.ability && effectPrefab)
        {
            player.CmdSetEmmisionColorOnPlayer(Color.white);
            CmdUsingEffect();
        }
    }

    [Command]
    private void CmdUsingEffect()
    {
        Debug.Log(this.isServer);
        ElectricObstacle electric = null;
        if(effectPrefab && effectPrefab.TryGetComponent<ElectricObstacle>(out electric))
        {
            animController.FireElectricAttack();
            electricVfx.Play();
            RpcUsingEffect(true);
        }
        else if (effectPrefab)
        {
            var effect = Instantiate(effectPrefab, transform.position, transform.rotation);
         
            NetworkServer.Spawn(effect);
            RpcUsingEffect(false);
        }

        effectPrefab = null;
    }

    [ClientRpc]
    private void RpcUsingEffect(bool electricAttack)
    {
        if (electricAttack)
        {
            animController.FireElectricAttack();
            electricVfx.Play();
        }
        else if (effectPrefab)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }

        effectPrefab = null;
    }

    [Client]
    public void SetEffectPrefab(GameObject prefab, Color lightColor)
    {
        if (effectPrefab != null) { return; }

        player.CmdSetEmmisionColorOnPlayer(lightColor);
        effectPrefab = prefab;
    }
  
}
