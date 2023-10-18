using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HintManager : MonoBehaviour
{
    [Tooltip("Hint Image")]
    [SerializeField]
    private GameObject hintImage;

    private int hintsPressed = 0;

    private bool CanPress = true;

    public void OnClick()
    {
        if (CanPress)
        {
            hintsPressed++;
            hintImage.SetActive(true);
            StartCoroutine(ShowHint());
        }
    }

    IEnumerator ShowHint()
    {
            yield return new WaitForSeconds(5f);
            DisableImage();      
    }

    public void DisableImage()
    {
        hintImage.SetActive(false);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public int GetHitnsPressed()
    {
        return hintsPressed;
    }

    public void SetCanPress(bool canPress)
    {
        this.CanPress = canPress;
    }
}
