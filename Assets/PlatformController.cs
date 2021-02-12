using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class PlatformController : NetworkBehaviour
{
    [SerializeField] private Animator[] platformsAnimator = null;
    [SerializeField] private float timeBtwPlatformsFalling = 0;
    private float currentTime = 0;

    [ServerCallback]
    private void Update()
    {
        if (timeBtwPlatformsFalling < currentTime)
        {
            int i = Random.Range(0, platformsAnimator.Length);
            int j = Random.Range(0, platformsAnimator.Length);

            platformsAnimator[i].Play("Fall");
            if (i != j)
            {
                platformsAnimator[j].Play("Fall");
            }

            currentTime = 0;
        }

        currentTime += Time.deltaTime;
    }
}
