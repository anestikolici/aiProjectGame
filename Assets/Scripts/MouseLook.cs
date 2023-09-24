using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivityX;
    [SerializeField] float mouseSensitivityY;
    float mouseX, mouseY;
    private Transform player;
    [SerializeField] Transform cam;
    float xRotation = 0f;
    [SerializeField] float xClamp = 85f; //optimization to not look behind

    
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, mouseX*Time.deltaTime);
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        cam.eulerAngles = targetRotation;
            
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x*mouseSensitivityX*Time.deltaTime;
        mouseY = mouseInput.y*mouseSensitivityY*Time.deltaTime;
      
    }
}
