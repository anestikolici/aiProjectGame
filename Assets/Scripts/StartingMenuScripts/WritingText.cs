using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WritingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    public string[] stringArray;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;

    //int i = 0;
    
    void Start()
    {
       EndCheck();
    }
    private void EndCheck()
    {
        StartCoroutine(TextVisible());
    }
    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
         int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
         int counter = 0;

         while (true)
         {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                Invoke("EndCheck", timeBetweenWords);
                break;
            }
            counter += 1;

            yield return new WaitForSeconds(timeBetweenCharacters);
         }
    }
}
