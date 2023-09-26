using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicTester : MonoBehaviour
{
   GameObject[] lights;
   public Material lightsOff;
   public Material lightsOn;

    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("lights");
  
    }

    // Update is called once per frame
    void Update()
    {
       foreach(GameObject light in lights)
        {
            if(light.GetComponent<MeshRenderer>().material == lightsOff)
            {
                light.GetComponent<MeshRenderer>().material = lightsOn;
            }
            
        }
       
    }
}
