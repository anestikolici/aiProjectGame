using UnityEngine;

// chord that plays when level gets cleared
public class AudioPlayerCleared : MonoBehaviour
{
    // LevelCleared AudioSource
    [Tooltip("LevelCleared AudioSource")]
    [SerializeField]
    private AudioSource audioSource;

    private bool hasPlayed = false;

    public void PlayAudio()
    {
        if (!audioSource.isPlaying && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}