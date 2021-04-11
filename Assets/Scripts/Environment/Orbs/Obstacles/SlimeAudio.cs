using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.Play();
    }
}
