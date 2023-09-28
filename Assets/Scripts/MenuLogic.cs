using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{
    // Input Manager script
    [Tooltip("Input Manager Script")]
    [SerializeField]
    private InputManager inputManager;

    // Player Movement script
    [Tooltip("Player Movement Script")]
    [SerializeField]
    private PlayerMovement playerMovement;

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

    // Start is called before the first frame update
    public void StartButton()
    {
        StartCoroutine(LerpPosition(new Vector3(0f, 0.67f, 0f), Quaternion.identity, 2));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
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

        cameraChildren.SetActive(true);
        inputManager.EnablePlayerInput();
        playerMovement.SetCameraOriginalPos();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ControlsButton()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void BackButton()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);     
    }
}
