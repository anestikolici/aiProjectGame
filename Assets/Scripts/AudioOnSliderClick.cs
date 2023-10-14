using UnityEngine;
using UnityEngine.UI;

public class AudioOnSliderClick : MonoBehaviour
{
    // Laser audio, since for now it is the loudest one
    [Tooltip("Laser audio, since for now it is the loudest one")]
    [SerializeField]
    private AudioSource laserAudio;

    // Audio slider
    [Tooltip("Audio slider")]
    [SerializeField]
    private Slider audioSlider;

    public void PlayAudioOnSliderClick()
    {
        laserAudio.volume = audioSlider.value;
        laserAudio.Play();
    }
}
