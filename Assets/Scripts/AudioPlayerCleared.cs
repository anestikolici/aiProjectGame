using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// chord that plays when level gets cleared
public class AudioPlayer : MonoBehaviour
{
    public logicFunctions LogicFunctions;
    public AudioClip soundClipcleared;
    private AudioSource audioSource;
    private bool hasPlayed = false;
    public TextMeshProUGUI messageText; // Reference to the Text GameObject for displaying messages
    public string messageContent = "Puzzle cleared! The door has opened"; 

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
            messageText.text = messageContent;
            messageText.enabled = true;
        }
    }
}