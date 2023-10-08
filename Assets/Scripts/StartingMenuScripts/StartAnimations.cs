using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class StartAnimations : MonoBehaviour
{
   private PlayerInputs mainMenuActions;
   private PlayerInputs.MainMenuActions _dancingActions;
   [SerializeField] GameObject player;

   void Awake()
   {
    mainMenuActions = new PlayerInputs();
    _dancingActions = mainMenuActions.MainMenu;
    _dancingActions.Enable();
    _dancingActions.Dance.performed += DancePerformed;

   }
  
   
    public void DancePerformed(InputAction.CallbackContext context)
    {
     player.GetComponent<Animator>().Play("Samba Dancing");
     //Debug.Log("Dance Performed" + context);
    }

   
        
}
