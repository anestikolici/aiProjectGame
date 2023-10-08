using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRotation : MonoBehaviour 
{
    public int Speed;
	private bool isOpen;
	
	// Update is called once per frame
	void Update ()
	
	{
        transform.Rotate(0,Speed*Time.deltaTime,0);
		isOpen = true;
	}

	void OnTriggerEnter(Collider other)
   {
    if(other.name == "Player" && isOpen)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
		Debug.Log("Collision");
    }
   }
}
