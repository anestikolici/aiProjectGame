using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool cameraControlEnabled = true;

    public Camera mainCamera;
    public Camera weaponCamera;
    private static float sensX = 1f;
    private static float sensY = 1f;
    float baseFov = 90f;
    float maxFov = 140f;
    float wallRunTilt = 15f;

    float wishTilt = 0;
    float curTilt = 0;
    Vector2 currentLook;
    Vector2 sway = Vector3.zero;
    float fov;
    Rigidbody rb;

    // Inverts vertical look
    private static bool invertY = false;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        curTilt = transform.localEulerAngles.z;
        currentLook.x = 180f;

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

    void Update()
    {
        if(cameraControlEnabled)
        {
            RotateMainCamera();
        }       
    }

    public void DisableCameraControl()
    {
        cameraControlEnabled = false;
    }

    public void EnableCameraControl()
    {
        cameraControlEnabled = true;
    }

    void FixedUpdate()
    {
        float addedFov = rb.velocity.magnitude - 3.44f;
        fov = Mathf.Lerp(fov, baseFov + addedFov, 0.5f);
        fov = Mathf.Clamp(fov, baseFov, maxFov);
        //mainCamera.fieldOfView = fov;
        //weaponCamera.fieldOfView = fov;

        currentLook = Vector2.Lerp(currentLook, currentLook + sway, 0.8f);
        curTilt = Mathf.LerpAngle(curTilt, wishTilt * wallRunTilt, 0.05f);

        sway = Vector2.Lerp(sway, Vector2.zero, 0.2f);
    }

    void RotateMainCamera()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseInput.x *= sensX;
        mouseInput.y *= sensY;

        currentLook.x += mouseInput.x;
        currentLook.y = Mathf.Clamp(currentLook.y += mouseInput.y, -60, 60);
        if (invertY)
            transform.localRotation = Quaternion.AngleAxis(currentLook.y, Vector3.right);
        else
            transform.localRotation = Quaternion.AngleAxis(-currentLook.y, Vector3.right);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, curTilt);
        transform.root.transform.localRotation = Quaternion.Euler(0, currentLook.x, 0);
    }

    public void UpdateSensitivities()
    {
        if (PlayerPrefs.HasKey("MouseSensitivityX"))
            sensX = PlayerPrefs.GetFloat("MouseSensitivityX") / 20f;
        if (PlayerPrefs.HasKey("MouseSensitivityY"))
            sensY = PlayerPrefs.GetFloat("MouseSensitivityY") / 20f;
    }

    public void InvertMouse()
    {
        invertY = !invertY;
    }

    public void Punch(Vector2 dir)
    {
        sway += dir;
    }

    #region Setters
    public void SetTilt(float newVal)
    {
        wishTilt = newVal;
    }

    public void SetXSens(float newVal)
    {
        sensX = newVal;
    }

    public void SetYSens(float newVal)
    {
        sensY = newVal;
    }

    public void SetFov(float newVal)
    {
        baseFov = newVal;
    }
    #endregion
}
