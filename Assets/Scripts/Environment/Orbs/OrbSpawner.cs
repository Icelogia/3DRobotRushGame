using UnityEngine;
using Mirror;

public class OrbSpawner : NetworkBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject[] orbsPrefabs;
    [Header("Parametres")]
    [SerializeField] private float radius = 1;
    private Vector3 centerPoint = Vector3.zero;

    [SerializeField] private int amountOfOrbsAtOneSpawn = 1;

    [SerializeField] private float timeBtwSpawningOrbs = 5;
    private float currentTime = 0;

    [Server]
    private void Update()
    {
        if(currentTime >= timeBtwSpawningOrbs)
        {
            SpawnOrbs();
            currentTime = 0;
        }

        currentTime += Time.deltaTime;
    }

    [Server]
    private void SpawnOrbs()
    {
        for(int i = 0; i < amountOfOrbsAtOneSpawn; i++)
        {
            int orbIndex = Random.Range(0, orbsPrefabs.Length);
            
            float xPos = Random.Range(-radius, radius);
            float yPos = Random.Range(-radius, radius);

            var orbObj = Instantiate(orbsPrefabs[orbIndex], new Vector3(xPos, 1, yPos), transform.rotation);

            NetworkServer.Spawn(orbObj);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 15);
        Gizmos.DrawSphere(centerPoint, radius);
    }
}
