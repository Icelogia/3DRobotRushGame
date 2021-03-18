using System.Collections;
using UnityEngine;
using Mirror;

public class PlayerEffectsController : NetworkBehaviour
{
    [SerializeField] private TrailRenderer[] trails = null;

    [Header("Eating orbs")]
    [SyncVar]
    [SerializeField] private float eatingRange = 1;
    [SyncVar]
    [SerializeField] private LayerMask orbsLayer;

    [SerializeField] private GameObject eatingEffect = null;
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform movementDirection = null;

    [ServerCallback]
    private void Update()
    {
        Eating();
    }

    [Server]
    private void Eating()
    {
        if (Physics.Raycast(trans.position, movementDirection.forward.normalized, eatingRange, orbsLayer))
        {
            eatingEffect.SetActive(true);
            RpcEating(true);
        }
        else
        {
            eatingEffect.SetActive(false);
            RpcEating(false);
        }
    }

    [ClientRpc]
    private void RpcEating(bool active)
    {
        eatingEffect.SetActive(active);
    }

    #region Trails
    [Server]
    public void TurnTrailsOn()
    {
        ChangeTrailsState(true);
        StartCoroutine(TurnTrailsOff());

        RpcTurnTrailsOn();
    }

    [ClientRpc]
    public void RpcTurnTrailsOn()
    {
        ChangeTrailsState(true);

        StartCoroutine(TurnTrailsOff());
    }

    private void ChangeTrailsState(bool state)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].emitting = state;
        }
    }
    private IEnumerator TurnTrailsOff()
    {
        yield return new WaitForSeconds(3);

        ChangeTrailsState(false);
    }
    #endregion Trails
}
