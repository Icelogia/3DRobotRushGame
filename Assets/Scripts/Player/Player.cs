using UnityEngine;
using Mirror;
using System;

public class Player : NetworkBehaviour
{
    
    [SyncVar]
    [SerializeField] private int maxEnergy = 100;
    [SyncVar]
    private int currentEnergy;

    

    private Camera mainCamera;
    

    

    
}
