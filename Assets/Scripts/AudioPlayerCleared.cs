using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// chord that plays when level gets cleared
public class AudioPlayer : MonoBehaviour
{
    public logicFunctions LogicFunctions;
    public AudioClip soundClipcleared;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClipcleared;

    }

    private void Update()
    {
        if (LogicFunctions.isSolved && !audioSource.isPlaying && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}