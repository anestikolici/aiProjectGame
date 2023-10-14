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

    [Tooltip("Controls Button")]
    [SerializeField]
    private GameObject controlsMenu;

    [Tooltip("Options Button")]
    [SerializeField]
    private GameObject optionsMenu;

    [Tooltip("Help Button")]
    [SerializeField]
    private GameObject helpMenu;

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
        if ((mainMenu.activeInHierarchy | controlsMenu.activeInHierarchy | optionsMenu.activeInHierarchy | helpMenu.activeInHierarchy) & !_timer.timerText.text.Equals("Time: 0:00"))
        {
            mainMenu.SetActive(false); //hide the main menu
            resumeButton.SetActive(false); //hide the resume button
            controlsMenu.SetActive(false); //hide the controls menu
            optionsMenu.SetActive(false); //hide the options menu
            helpMenu.SetActive(false); //hide the help menu

            mouseLook.EnableMouseLook(); // Enable mouse look
            inputManager.EnablePlayerInput(); // Enable player input

            _timer.StartTimer(); // Enable timer

            crosshair.SetActive(true); //show the crosshair  

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!_timer.timerText.text.Equals("Time: 0:00"))
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

    public void DisablePause()
    {
        _pause.Disable();
    }

     
}
