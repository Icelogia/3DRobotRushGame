using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private AudioClip staticEngineSound = null;
    [SerializeField] private AudioClip movingEngineSound = null;

    private void Start()
    {
        audioSource.clip = staticEngineSound;
        audioSource.Play();
    }

    private void Update()
    {
        var movment = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        if(movment.magnitude > 0.4)
        {
            ChangeSound(movingEngineSound);
        }
        else
        {
            ChangeSound(staticEngineSound);
        }
    }

    private void ChangeSound(AudioClip engineSound)
    {
        if(engineSound != audioSource.clip)
        {
            audioSource.clip = engineSound;
            audioSource.Play();
        }
    }
}
