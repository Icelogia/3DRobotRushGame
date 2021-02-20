﻿using UnityEngine;
using Mirror;

public class OrbSpawner : NetworkBehaviour
{
    [SerializeField] private bool gizmosOn = true;

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
            
            float xPos = Random.Range(0, radius);
            float yPos = Random.Range(0, radius);

            var orbObj = Instantiate(orbsPrefabs[orbIndex], new Vector3(xPos, 1, yPos), transform.rotation);

            NetworkServer.Spawn(orbObj);
        }
    }

    private void OnDrawGizmos()
    {
        if(!gizmosOn) { return; }

        Gizmos.color = new Color(255, 0, 0, 15);
        Gizmos.DrawSphere(centerPoint, radius);
    }
}
