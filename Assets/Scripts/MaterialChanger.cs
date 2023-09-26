using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material lightOff;
    public Material lightOn;

    void OnTriggerEnter(Collider other)
    {
            if(GetComponent<MeshRenderer>().sharedMaterial == lightOff)
            {
               GetComponent<MeshRenderer>().sharedMaterial = lightOn;
            }

            else
        {
            GetComponent<MeshRenderer>().sharedMaterial = lightOff;
        }

            
        
        
    }
}
