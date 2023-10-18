using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Camera attached to the player
    [Tooltip("Camera attached to the player")]
    [SerializeField]
    private Transform cam;

    // Inverts vertical look
    private static bool invertY = false;

    // Mouse X and Y values
    [HideInInspector]
    public float mouseX, mouseY;

    // Mouse sensitivity for the X-axis
    private static float mouseSensitivityX = 50f;

    // Mouse sensitivity for the Y-axis
    private static float mouseSensitivityY = 50f;

    // Controls if the player can look around
    private bool canLook;

    void Start()
    {
        // Initially, prevent mouse input
        canLook = false;

        // Retrieve mouse sensitivities
        UpdateSensitivities();

        if (PlayerPrefs.HasKey("InvertMouse"))
        {
            int invert = PlayerPrefs.GetInt("InvertMouse");
            if (invert == 0)
                invertY = true;
            else
                invertY = false;
        }
    }

    public void EnableMouseLook()
    {
        //yield return new WaitForSeconds(0.1f);

        // Allow mouse input
        canLook = true;
    }

    public void DisableMouseLook()
    {
        canLook = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canLook)
        {
            // Horizontal mouse look
            transform.Rotate(0f, mouseX, 0f);

            // Vertical mouse look
            if (invertY)
                cam.transform.Rotate(mouseY, 0f, 0f);
            else
                cam.transform.Rotate(-mouseY, 0f, 0f);

            // Restricting vertical camera movement
            if (cam.transform.localEulerAngles.x > 60f && cam.transform.localEulerAngles.x < 100f)
                cam.transform.localEulerAngles = new Vector3(60f, cam.transform.localEulerAngles.y,
                    cam.transform.localEulerAngles.z);
            else if (cam.transform.localEulerAngles.x < 285f && cam.transform.localEulerAngles.x > 150f)
                cam.transform.localEulerAngles = new Vector3(285f, cam.transform.localEulerAngles.y,
                    cam.transform.localEulerAngles.z);
        }
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * mouseSensitivityX * Time.deltaTime;
        mouseY = mouseInput.y * mouseSensitivityY * Time.deltaTime;    
    }

    public void UpdateSensitivities()
    {
        mouseSensitivityX = PlayerPrefs.GetFloat("MouseSensitivityX");
        mouseSensitivityY = PlayerPrefs.GetFloat("MouseSensitivityY");
    }

    public void InvertMouse()
    {
        invertY = !invertY;
    }
}
