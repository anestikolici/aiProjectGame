using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Character Movement Horizontally
    Vector2 movement;
    [SerializeField] CharacterController controller;
    [SerializeField] float movementspeed = 10f;

    //Character Movement Vertically
    [SerializeField] float gravity = -10f; //so it will not stay up
    Vector3 jumpVelocity = Vector3.zero; //no velocity by default
    [SerializeField] float jumpHeight = 3f;
    bool isJumping;

    //Optimize character falling back to the ground
    [SerializeField] LayerMask ground;
    bool isGrounded;
        

    
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
        Vector3 movementVelocity =(transform.right * movement.x + transform.forward * movement.y) * movementspeed;
        controller.Move(movementVelocity*Time.deltaTime);

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

        
    }

    public void ReceiveInput(Vector2 _movement)
    {
        movement = _movement;

    }

    public void OnJumpPressed()
    {
        isJumping = true;
    }
}
