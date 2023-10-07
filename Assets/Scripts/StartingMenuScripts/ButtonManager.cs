using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Main Menu Panel
    [Tooltip("Main Menu Panel")]
    [SerializeField]
    private GameObject mainMenu;

    // Controls Menu Panel
    [Tooltip("Controls Menu Panel")]
    [SerializeField]
    private GameObject controlsMenu;

    //Game Goal Panel
    [Tooltip("Game Goal Panel")]
    [SerializeField]
    private GameObject goalPanel; 

    //Player
    [Tooltip("Player")]
    [SerializeField]
    private GameObject player; 


    void Start()
    {
        StartCoroutine(MenuAppearrance());
    }

    IEnumerator MenuAppearrance()
    {
        yield return new WaitForSeconds(5.5f);
        mainMenu.SetActive(true);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnControlsButton()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void OnGoalButton()
    {
        mainMenu.SetActive(false);
        player.SetActive(false);
        goalPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        controlsMenu.SetActive(false);
        goalPanel.SetActive(false);
        mainMenu.SetActive(true);
        player.SetActive(true);
    }
}
