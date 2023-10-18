using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HintManager : MonoBehaviour
{
    [Tooltip("Hint Image")]
    [SerializeField]
    private GameObject hintImage;

    public void OnClick()
    {
        hintImage.SetActive(true);
        StartCoroutine(ShowHint());
    }

    IEnumerator ShowHint()
    {
       
           

            yield return new WaitForSeconds(5f);
            hintImage.SetActive(false);

        
    }
}
