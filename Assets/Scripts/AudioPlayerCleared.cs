using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// chord that plays when level gets cleared
public class AudioPlayerCleared : MonoBehaviour
{
    public AudioClip soundClipcleared;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClipcleared;

    }

    public void PlayAudio()
    {
        if (!audioSource.isPlaying && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}