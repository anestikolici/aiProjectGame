using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Tooltip("Player Movement Script")]
    [SerializeField] 
    private PlayerMovement playerMovement;

    [Tooltip("Player Σηοοτινγ Script")]
    [SerializeField]
    private PlayerShooting playerShooting;

    [Tooltip("Mouse Look Script")]
    [SerializeField] 
    private MouseLook mouseLook;

    [Tooltip("Weapon Controller Script")]
    [SerializeField]
    private WeaponController weaponController;

    private PlayerInputs controls;

    private Vector2 movement;
    private Vector2 mouseInput;

    private PlayerInputs.PlayerActions player;
    
    void Awake()
    {
        controls = new PlayerInputs();
        player = controls.Player;

        player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        player.Jump.performed += _ => playerMovement.OnJumpPressed();
        player.Mouse.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

        player.Fire.performed += _ => playerShooting.FirePressed();
        player.FireReleased.performed += _ => playerShooting.FireReleased();
        player.Reload.performed += _ => playerShooting.OnReloadPressed();
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
