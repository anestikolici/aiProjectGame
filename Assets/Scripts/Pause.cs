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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
        mainMenu.SetActive(true);
        playButton.SetActive(false);
        resumeButton.SetActive(true);
        mouseLook.DisableMouseLook();

        crosshair.SetActive(false); //hide the crosshair

        
        
       
    }

     
}
