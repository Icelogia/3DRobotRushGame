using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        AudioClip clip = DrawMusicClip();
        PlaySpecificMusic(clip);
    }

    private AudioClip DrawMusicClip()
    {
        int musicIndex = Random.Range(0, audioClips.Length);
        return audioClips[musicIndex];
    }

    private void PlaySpecificMusic(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(PlayNextMusicAfter(audioClip.length));
    }

    private IEnumerator PlayNextMusicAfter(float time)
    {
        yield return new WaitForSeconds(time);

        AudioClip clip = DrawMusicClip();
        PlaySpecificMusic(clip);
    }
}
