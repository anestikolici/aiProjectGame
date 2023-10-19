using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private string previousCollider = "";

    private int numberOfFalls = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Non-Platform") && previousCollider == "Platform")
            numberOfFalls++;
        previousCollider = collision.collider.tag;
    }

    public int GetNumberOfFalls()
    {
        return numberOfFalls;
    }
}
