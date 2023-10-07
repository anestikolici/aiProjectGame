using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeText : MonoBehaviour
{
    private float time = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}
