using UnityEngine;

/// <summary>
/// Weapon controller scripts
/// </summary>
public class WeaponController : MonoBehaviour
{
    #region - SerializeFields -
    // Player
    [Tooltip("Player GameObject")]
    [SerializeField]
    private GameObject player;

    // Sway anchor
    [Tooltip("Sway anchor")]
    [SerializeField]
    private Transform swayAnchor;

    [Header("Sway Settings")]
    // Hip fire sway intensity
    [Tooltip("Hip fire sway Intensity")]
    [SerializeField]
    private float hipSwayIntensity;

    // Sway smoothing
    [Tooltip("Sway smoothing")]
    [SerializeField]
    private float swaySmooth;

    // Mouse Look Script
    [Tooltip("Mouse Look Script")]
    [SerializeField]
    private MouseLook mouseLook;
    #endregion

    #region - Non SerializeFields -
    // Current sway intensity
    private float swayIntensity;

    // Original weapon sway anchor rotation
    private Quaternion originalSwayRotation;

    #endregion

    #region - Start/Update -
    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {     
        originalSwayRotation = swayAnchor.localRotation;
        swayIntensity = hipSwayIntensity;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // Weapon sway
        Sway();
    }
    #endregion

    #region - Weapon Sway -
    /// <summary>
    /// Weapon sway
    /// </summary>
    private void Sway()
    {
        float mouseX = mouseLook.mouseX;
        float mouseY = mouseLook.mouseY;
        swayIntensity = hipSwayIntensity;
        Quaternion swayAdjustmentX = Quaternion.AngleAxis(-swayIntensity * mouseY, Vector3.up);
        Quaternion swayAdjustmentY = Quaternion.AngleAxis(swayIntensity * mouseX, Vector3.right);
        Quaternion swayRotation = originalSwayRotation * swayAdjustmentX * swayAdjustmentY;

        swayAnchor.localRotation = Quaternion.Lerp(swayAnchor.localRotation, swayRotation, Time.deltaTime * swaySmooth);
    }
    #endregion
}
