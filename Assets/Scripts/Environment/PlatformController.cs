using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class PlatformController : NetworkBehaviour
{
    [SerializeField] private int amountOfPlatformsToFall = 0;

    [Header("Platforms")]
    private Animator[] platformsAnimator = null;
    private List<int> isInAnimation = new List<int>();

    [Header("Parametres")]
    [SerializeField] private float timeBtwPlatformsFalling = 0;
    [SerializeField] private bool isPlatformsFallable = true;
    private float currentTime = 0;

    [ServerCallback]
    private void Start()
    {
        platformsAnimator = GetComponentsInChildren<Animator>();
    }

    [ServerCallback]
    private void Update()
    {
        if (!isPlatformsFallable) { return; }

        if (timeBtwPlatformsFalling < currentTime)
        {
            isInAnimation.Clear();

            for (int i = 0; i < amountOfPlatformsToFall; i++)
            {
                int j = Random.Range(0, platformsAnimator.Length);
                if (!isInAnimation.Contains(j))
                {
                    platformsAnimator[j].Play("Fall");
                    isInAnimation.Add(j);
                } 
            }

            currentTime = 0;
        }

        currentTime += Time.deltaTime;
    }
}
