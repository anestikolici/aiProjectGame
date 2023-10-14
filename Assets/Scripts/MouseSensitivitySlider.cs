using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour
{
    // Mouse sensitivity slider for one Axis
    [Tooltip("Mouse sensitivity slider for one Axis")]
    [SerializeField]
    private Slider mouseSensitivitySlider;

    // Mouse axis that this slider modifies
    [Tooltip("Mouse axis that this slider modifies")]
    [SerializeField]
    private string mouseAxis;

    // MouseLook script reference
    [Tooltip("MouseLook script reference")]
    [SerializeField]
    private MouseLook mouseLook;

    // Current audio volume
    private float mouseSensitivity;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity" + mouseAxis))
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity" + mouseAxis);
        else
            mouseSensitivity = 100;

        mouseSensitivitySlider.value = mouseSensitivity;
    }

    /// <summary>
    /// Update player volume preferences every time when the value of the attached slider is changes
    /// </summary>
    public void OnValueChanged()
    {
        PlayerPrefs.SetFloat("MouseSensitivity" + mouseAxis, mouseSensitivitySlider.value);
        PlayerPrefs.Save();

        if (mouseLook != null)
            mouseLook.UpdateSensitivities();
    }
}
