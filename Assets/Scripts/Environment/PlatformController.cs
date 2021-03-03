using UnityEngine;
using Mirror;

public class PlatformController : NetworkBehaviour
{
    [Header("Platforms")]
    [SerializeField] private Animator[] platformsAnimator = null;

    [Header("Parametres")]
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
