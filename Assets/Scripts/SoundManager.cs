using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void AudioPlay(AudioClip clip)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;

        audioSource.PlayDelayed(1f);
    }

    public void AudioStop()
    {
        audioSource.Stop();
    }
}
