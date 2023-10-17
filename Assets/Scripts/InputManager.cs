using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Tooltip("Player Movement Script")]
    [SerializeField] 
    private PlayerMovement playerMovement;

    [Tooltip("Player Shooting Script")]
    [SerializeField]
    private PlayerShooting playerShooting;

    [Tooltip("Mouse Look Script")]
    [SerializeField] 
    private MouseLook mouseLook;    

    private PlayerInputs controls;

    private Vector2 movement;
    private Vector2 mouseInput;

    private PlayerInputs.PlayerActions player;

    void Update()
    {
        playerMovement.ReceiveInput(movement);
        mouseLook.ReceiveInput(mouseInput);
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    public void EnablePlayerInput()
    {
        controls = new PlayerInputs();
        player = controls.Player;

        player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        player.Jump.performed += _ => playerMovement.OnJumpPressed();
        player.Mouse.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

        if (playerShooting != null)
        {
            player.Fire.performed += _ => playerShooting.FirePressed();
            player.FireReleased.performed += _ => playerShooting.FireReleased();
            player.Reload.performed += _ => playerShooting.OnReloadPressed();
        }

        // Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls.Enable();
    }
}
