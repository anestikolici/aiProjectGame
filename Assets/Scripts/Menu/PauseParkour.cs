using UnityEngine;
using UnityEngine.InputSystem;

public class PauseParkour : MonoBehaviour
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

    [Tooltip("CameraController script")]
    [SerializeField]
    private CameraController cameraController;

    [Tooltip("Crosshair")]
    [SerializeField]
    private GameObject crosshair;

    
    [Tooltip("Input Manager script")]
    [SerializeField]
    private Timer _timer;

    /*private PlayerInputs mainMenuActions;
    private PlayerInputs.MainMenuActions _pause;*/

    private bool isPaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        /*mainMenuActions = new PlayerInputs();
        _pause = mainMenuActions.MainMenu;
        _pause.Enable();
        _pause.Pause.performed += PausePerformed;    */  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                DisablePause();
                isPaused = false;
            }
            else
            {
                PauseGame();
                isPaused = true;
            }
        }
    }
    // Update is called once per frame
    public void PauseGame()
    {
        Debug.Log("ESC");

        Cursor.lockState = CursorLockMode.None; //show the cursor so the player can select options
        Cursor.visible = true;

        mainMenu.SetActive(true); //show the main menu
        playButton.SetActive(false); //hide the play button
        resumeButton.SetActive(true); //show the resume button
        controlsMenu.SetActive(true); //hide the controls menu
        optionsMenu.SetActive(true); //hide the options menu

        cameraController.DisableCameraControl(); //stop camera movement according to mouse position

        _timer.PauseTimer(); //pause the timer

        crosshair.SetActive(false); //hide the crosshair  
        Debug.Log("second if");

        
    }

    public void DisablePause()
    {
        
        Debug.Log("exit");

        mainMenu.SetActive(false); //hide the main menu
        resumeButton.SetActive(false); //hide the resume button
        controlsMenu.SetActive(false); //hide the controls menu
        optionsMenu.SetActive(false); //hide the options menu
        helpMenu.SetActive(false); //hide the help menu

        cameraController.EnableCameraControl(); // Enable mouse look

        _timer.StartTimer(); // Enable timer

        crosshair.SetActive(true); //show the crosshair  

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("first if");
    }

     
}
