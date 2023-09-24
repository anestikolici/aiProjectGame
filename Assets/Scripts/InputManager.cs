using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] MouseLook mouseLook;
    PlayerInputs controls;

    Vector2 movement;
    Vector2 mouseInput;

    PlayerInputs.PlayerActions player;
    
    void Awake()
    {
        controls = new PlayerInputs();
        player = controls.Player;

        player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        player.Jump.performed += _ => playerMovement.OnJumpPressed();
        player.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        player.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

    }

    void Update()
    {
        playerMovement.ReceiveInput(movement);
        mouseLook.ReceiveInput(mouseInput);
    }

    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
}
