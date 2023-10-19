using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertMouse : MonoBehaviour
{
    // Invert Mouse checkbox
    [Tooltip("Invert Mouse checkbox")]
    [SerializeField]
    private Toggle invertMouse;

    // MouseLook script reference
    [Tooltip("MouseLook script reference")]
    [SerializeField]
    private MouseLook mouseLook;

    // CameraController script reference
    [Tooltip("CameraController script reference")]
    [SerializeField]
    private CameraController cameraController;


    // Represents a boolean: 0=inverted, 1=normal
    private static int invert;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("InvertMouse"))
        {
            invert = PlayerPrefs.GetInt("InvertMouse");
            if (invert == 0)
                invertMouse.SetIsOnWithoutNotify(true);
            else
                invertMouse.SetIsOnWithoutNotify(false);
        }
        else
        {
            invertMouse.SetIsOnWithoutNotify(false);
            invert = 1;
            PlayerPrefs.SetInt("InvertMouse", invert);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Update player volume preferences every time when the value of the attached slider is changes
    /// </summary>
    public void OnValueChanged()
    {
        if (invert == 0)
            invert = 1;
        else
            invert = 0;
        PlayerPrefs.SetInt("InvertMouse", invert);
        PlayerPrefs.Save();

        if (mouseLook != null)
            mouseLook.InvertMouse();
        else if (cameraController != null)
            cameraController.InvertMouse();
    }
}
