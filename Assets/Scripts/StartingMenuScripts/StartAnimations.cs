using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class StartAnimations : MonoBehaviour
{
   

    public GameObject player;
  
    // Update is called once per frame
    public void Dance(InputAction.CallbackContext context)
    {
     player.GetComponent<Animator>().Play("Dance");
    }

   
        
}
