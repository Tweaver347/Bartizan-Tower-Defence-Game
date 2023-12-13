using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public AudioSource audioSource;
    public void Start()
    {
        audioSource.Play();
    }

    public void play()
    {
        audioSource.Play();
    }

    public void stop()
    {
        audioSource.Stop();
    }

    public void pause()
    {
        audioSource.Pause();
    }

    public void unpause()
    {
        audioSource.UnPause();
    }

    public void mute()
    {
        audioSource.mute = true;
    }

    public void unmute()
    {
        audioSource.mute = false;
    }

    public void PlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
