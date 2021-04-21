using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
