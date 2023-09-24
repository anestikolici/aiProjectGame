using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Camera attached to the player
    [Tooltip("Camera attached to the player")]
    [SerializeField]
    private Transform cam;

    // Horizontal mouse sensitivity
    [Tooltip("Horizontal mouse sensitivity")]
    [Range(1, 100)]
    [SerializeField]
    private int mouseSensitivityX;

    // Vertical mouse sensitivity
    [Tooltip("Vertical mouse sensitivity")]
    [Range(1, 100)]
    [SerializeField]
    private int mouseSensitivityY;

    // Inverts vertical look
    [Tooltip("Inverts vertical look")]
    [SerializeField]
    private bool invertY = false;

    // Mouse X and Y values
    [HideInInspector]
    public float mouseX, mouseY;

    // Solves weird issue when clicking play that causes the character not look at (0, 0, 0)
    private bool canLook = false;

    void Start()
    {
        // When the game starts, the player looks ath the middle of the camera
        cam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        // Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initially, prevent mouse input
        canLook = false;

        // Enable mouse input after 0.1 seconds
        StartCoroutine(EnableMouseLook());
    }

    IEnumerator EnableMouseLook()
    {
        yield return new WaitForSeconds(0.1f);

        // Allow mouse input
        canLook = true;
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
                cam.transform.Rotate(-mouseY, 0f, 0f);
            else
                cam.transform.Rotate(mouseY, 0f, 0f);

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
}
