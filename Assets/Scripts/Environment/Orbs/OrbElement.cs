using UnityEngine;
using Mirror;
using System.Collections;

public class OrbElement : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float lifeTime = 5f;

    [ServerCallback]
    private void Start()
    {
        StartCoroutine(Life());
    }

    [Server]
    private IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        NetworkServer.Destroy(this.gameObject);
    }
}
