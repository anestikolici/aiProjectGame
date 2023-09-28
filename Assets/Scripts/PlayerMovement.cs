using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Character Movement Horizontally
    Vector2 movement;
    [SerializeField] CharacterController controller;

    // Player movement speed
    [Tooltip("Player movement speed")]
    [SerializeField] 
    float movementSpeed = 10f;

    // Weapon anchor GameObject for headbobbing
    [Tooltip("Weapon anchor GameObject for headbobbing")]
    [SerializeField]
    private GameObject headbobbingAnchor;

    //Character Movement Vertically
    [SerializeField] float gravity = -10f; //so it will not stay up
    Vector3 jumpVelocity = Vector3.zero; //no velocity by default
    [SerializeField] float jumpHeight = 3f;
    bool isJumping;

    //Optimize character falling back to the ground
    [SerializeField] LayerMask ground;
    bool isGrounded;

    // Weapon original position
    private Vector3 headbobAnchorOriginalPos;

    // Camera original position
    private Vector3 cameraOriginalPos;

    // Counter for idle headbobbing
    private float idleCounter;

    // Counter for moving headbobbing
    private float movementCounter;


    private void Start()
    {
        headbobAnchorOriginalPos = headbobbingAnchor.transform.localPosition;
    }

    void Update()
    {
        float halfHeight = controller.height * 0.5f;
        var bottomPoint = transform.TransformPoint(controller.center - Vector3.up * halfHeight);
        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, ground);
        if (isGrounded)
        {
            jumpVelocity.y = 0f;
        }
        //managing the movement horizontally
        Vector3 movementVelocity = (transform.right * movement.x + transform.forward * movement.y) * movementSpeed;
        controller.Move(movementVelocity * Time.deltaTime);

        //managing the movement vertically
        if (isJumping)
        {
            if (isGrounded)
            {
                jumpVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity); //mathematical equation
            }
            isJumping = false;
        }

        jumpVelocity.y += gravity * Time.deltaTime;
        controller.Move(jumpVelocity * Time.deltaTime);

        // Idle HeadBobbing
        if (movement.Equals(new Vector2(0f, 0f)))
        {
            HeadBobbing(idleCounter, 0.009f, 0.001f);
            idleCounter += Time.deltaTime * 2f;
        }
        // HeadBobbing when moving
        else
        {
            HeadBobbing(movementCounter, 0.011f, 0.004f);
            CameraBobbing(movementCounter, 0.001f, 0.06f);
            movementCounter += Time.deltaTime * movementSpeed;
        }     
    }

    public void SetCameraOriginalPos()
    {
        cameraOriginalPos = Camera.main.transform.localPosition;
    }

    public void ReceiveInput(Vector2 _movement)
    {
        movement = _movement;
    }

    public void OnJumpPressed()
    {
        isJumping = true;
    }

    #region - Camera/Head Bobbing -
    /// <summary>
    /// Player headbobbing
    /// </summary>
    /// <param name="input">Input of the sin/cos functions</param>
    /// <param name="verticalIntensity">Intensity of vertical headbobbing</param>
    /// <param name="horizontalIntensity">Intensity of horizontal headbobbing</param>
    private void HeadBobbing(float input, float verticalIntensity, float horizontalIntensity)
    {
        Vector3 targetBobPosition = headbobAnchorOriginalPos + new Vector3(Mathf.Cos(input) * verticalIntensity, Mathf.Sin(2 * input) * horizontalIntensity, 0);
        headbobbingAnchor.transform.localPosition = Vector3.Lerp(headbobbingAnchor.transform.localPosition, targetBobPosition, Time.deltaTime * 2f);
    }

    /// <summary>
    /// Camera headbobbing
    /// </summary>
    /// <param name="input">Input of the sin/cos functions</param>
    /// <param name="verticalIntensity">Intensity of vertical headbobbing</param>
    /// <param name="horizontalIntensity">Intensity of horizontal headbobbing</param>
    private void CameraBobbing(float input, float verticalIntensity, float horizontalIntensity)
    {
        Vector3 targetCameraBobPosition = cameraOriginalPos + new Vector3(Mathf.Cos(input) * verticalIntensity, Mathf.Sin(2 * input) * horizontalIntensity, 0);
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetCameraBobPosition, Time.deltaTime * 7f);
    }

    #endregion
}
