using UnityEngine;
using Mirror;

[RequireComponent(typeof(Collider))]
public class Orb : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private GameObject effectPrefab = null;
}
