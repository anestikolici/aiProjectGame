using UnityEngine;
using UnityEngine.ProBuilder;

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

    [Tooltip("PlayerMovement1 script")]
    [SerializeField]
    private PlayerMovement1 playerMovement;

    [Tooltip("Shooting script")]
    [SerializeField]
    private Shooting shooting;

    [Tooltip("Crosshair")]
    [SerializeField]
    private GameObject crosshair;

    public GameObject Player; //for removing gun on pause

    [Tooltip("Input Manager script")]
    [SerializeField]
    private Timer _timer;

    /*private PlayerInputs mainMenuActions;
    private PlayerInputs.MainMenuActions _pause;*/

    private bool isPaused = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused & (mainMenu.activeInHierarchy | controlsMenu.activeInHierarchy | optionsMenu.activeInHierarchy | helpMenu.activeInHierarchy) & _timer.ElapsedTime < 300f)
            {
                DisablePause();
                isPaused = false;
            }
            else if (_timer.ElapsedTime < 300f)
            {
                PauseGame();
                isPaused = true;
            }
        }
    }
    // Update is called once per frame
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; //show the cursor so the player can select options
        Cursor.visible = true;

        mainMenu.SetActive(true); //show the main menu
        playButton.SetActive(false); //hide the play button
        resumeButton.SetActive(true); //show the resume button

        shooting.SetCanShoot(false);
        cameraController.DisableCameraControl(); //stop camera movement according to mouse position
        playerMovement.DisableInput();

        _timer.PauseTimer(); //pause the timer

        crosshair.SetActive(false); //hide the crosshair         
    }

    public void DisablePause()
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(false); //hide the main menu
        resumeButton.SetActive(false); //hide the resume button
        controlsMenu.SetActive(false); //hide the controls menu
        optionsMenu.SetActive(false); //hide the options menu
        helpMenu.SetActive(false); //hide the help menu

        cameraController.EnableCameraControl(); // Enable mouse look
        playerMovement.EnableInput();
        shooting.SetCanShoot(true);

        _timer.StartTimer(); // Enable timer

        crosshair.SetActive(true); //show the crosshair  

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

     
}
