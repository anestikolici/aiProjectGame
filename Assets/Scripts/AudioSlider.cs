using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    // Audio slider
    [Tooltip("Audio slider")]
    [SerializeField]
    private Slider audioSlider;

    // List of all AudioSources in this scene
    private AudioSource[] audioSources;

    // Current audio volume
    private float audioVolume;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        if (PlayerPrefs.HasKey("AudioPref"))
            audioVolume = PlayerPrefs.GetFloat("AudioPref");
        else
            audioVolume = 1.0f;

        audioSlider.value = audioVolume;

        audioSources = FindObjectsOfType<AudioSource>();
        if (audioSources != null)
            foreach (AudioSource a in audioSources)
                a.volume = audioVolume;
    }

    /// <summary>
    /// Update player volume preferences every time when the value of the attached slider is changes
    /// </summary>
    public void OnValueChanged()
    {
        PlayerPrefs.SetFloat("AudioPref", audioSlider.value);
        PlayerPrefs.Save();

        if (audioSources != null)
            foreach (AudioSource a in audioSources)
            a.volume = audioSlider.value;
    }
}
