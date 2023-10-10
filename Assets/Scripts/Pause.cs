using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [Tooltip("Main Menu Panel")]
    [SerializeField]
    private GameObject mainMenu;

    [Tooltip("Play Button")]
    [SerializeField]
    private GameObject playButton;

    [Tooltip("Resume Button")]
    [SerializeField]
    private GameObject resumeButton;

    [Tooltip("MouseLook script")]
    [SerializeField]
    private MouseLook mouseLook;

    [Tooltip("Crosshair")]
    [SerializeField]
    private GameObject crosshair;

    [Tooltip("Input Manager script")]
    [SerializeField]
    private InputManager inputManager;

    [Tooltip("Input Manager script")]
    [SerializeField]
    private Timer _timer;






    private PlayerInputs mainMenuActions;
    private PlayerInputs.MainMenuActions _pause;
    // Start is called before the first frame update
    void Awake()
    {
    mainMenuActions = new PlayerInputs();
    _pause = mainMenuActions.MainMenu;
    _pause.Enable();
    _pause.Pause.performed += PausePerformed;
    
        
    }

    // Update is called once per frame
    public void PausePerformed(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None; //show the cursor so the player can select options
        Cursor.visible = true;
       
        mainMenu.SetActive(true); //show the main menu
        playButton.SetActive(false); //hide the play button
        resumeButton.SetActive(true); //show the resume button

        mouseLook.DisableMouseLook(); //stop camera movement according to mouse position
        inputManager.OnDisable(); //stop shooting, moving etc when in pause mode

        _timer.PauseTimer(); //pause the timer

        crosshair.SetActive(false); //hide the crosshair

        
        
       
    }

     
}
