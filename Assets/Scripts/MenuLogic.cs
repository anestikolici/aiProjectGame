using System.Collections;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{
     public Timer timer;
    // Input Manager script
    [Tooltip("Input Manager Script")]
    [SerializeField]
    private InputManager inputManager;

    // Player Movement script
    [Tooltip("Player Movement Script")]
    [SerializeField]
    private PlayerMovement playerMovement;

    // MouseLook script
    [Tooltip("MouseLook script")]
    [SerializeField]
    private MouseLook mouseLook;

    // Main camera
    [Tooltip("Main Camera")]
    [SerializeField]
    private Camera mainCamera;

    // Children of the main camera
    [Tooltip("Children of the main camera")]
    [SerializeField]
    private GameObject cameraChildren;

    // Main Menu Panel
    [Tooltip("Main Menu Panel")]
    [SerializeField]
    private GameObject mainMenu;

    // Controls Menu Panel
    [Tooltip("Controls Menu Panel")]
    [SerializeField]
    private GameObject controlsMenu;

    // Options Menu Panel
    [Tooltip("Options Menu Panel")]
    [SerializeField]
    private GameObject optionsMenu;

    [Tooltip("Game Goal Panel")]
    [SerializeField]
    private GameObject goalPanel;

    [Tooltip("Hint button")]
    [SerializeField]
    private HintManager hintButton;

    [Tooltip("Ammo Count")]
    [SerializeField]
    private GameObject ammoCountPanel;

    [Tooltip("Pause")]
    [SerializeField]
    private GameObject pausePanel;

    [Tooltip("Crosshair")]
    [SerializeField]
    private GameObject crosshair;

    // Controls if the menu buttons can be pressed
    private bool canPress = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    public void StartButton()
    {
        StartCoroutine(LerpPosition(new Vector3(0f, 0.67f, 0f), Quaternion.identity, 2));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        canPress = false;
        if (hintButton != null)
            hintButton.SetCanPress(false);
        float time = 0;
        Vector3 startPosition = mainCamera.transform.localPosition;
        Quaternion startRotation = mainCamera.transform.localRotation;
        while (time < duration)
        {
            mainCamera.transform.SetLocalPositionAndRotation(Vector3.Lerp(startPosition, targetPosition, time / duration), 
                Quaternion.Lerp(startRotation, targetRotation, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.SetLocalPositionAndRotation(targetPosition, targetRotation);

        if (cameraChildren != null)
            cameraChildren.SetActive(true);
        inputManager.EnablePlayerInput();
        playerMovement.SetCameraOriginalPos();
        mouseLook.EnableMouseLook();
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        // start timer after the start button is pressed 
        if (timer != null)
        {
            timer.StartTimer();
        }
        canPress = true;

        if (hintButton != null)
        {
            hintButton.SetCanPress(true);
            hintButton.DisableImage();
        }
            
    }

    public void ControlsButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            controlsMenu.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void OptionsButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void BackButton()
    {
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        goalPanel.SetActive(false);
        mainMenu.SetActive(true);   
        ammoCountPanel.SetActive(true);
        pausePanel.SetActive(true); 
        crosshair.SetActive(true);
        crosshair.SetActive(false);
         
    }

    public void GoalButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            goalPanel.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainMenu.SetActive(false);
        mouseLook.EnableMouseLook();
        crosshair.SetActive(true);
        inputManager.EnablePlayerInput();
        timer.StartTimer();
    }
}
