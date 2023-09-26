using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material lightOff;
    public Material lightOn;

    //testing the arrays
    GameObject[] lights;
    GameObject[] lightsOn;
    GameObject selectedcube;
    int index;
    public int hitCounts = 0;
    GameObject parentGameObject;
    GameObject[] nextParentGameObject;

    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("lights");
        index = Random.Range(0, lights.Length);
        selectedcube = lights[index];
        //print(selectedcube.name);
        selectedcube.GetComponent<MeshRenderer>().material= lightOn; //opening randomly some lights at the begining of the game
        parentGameObject = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {

        //not deleting it because i want it as reference
        /*foreach (GameObject light in lights)
        {
            if (light.GetComponent<MeshRenderer>().sharedMaterial == lightOff)
            {
                light.GetComponent<MeshRenderer>().sharedMaterial = lightOn;
            }

            else
            {
               light.GetComponent<MeshRenderer>().sharedMaterial = lightOff;
            }
        }*/

        if (GetComponent<MeshRenderer>().sharedMaterial == lightOff)
        {
            GetComponent<MeshRenderer>().sharedMaterial = lightOn;

        }

        else
        {
            GetComponent<MeshRenderer>().sharedMaterial = lightOff;
        }

       hitCounts++;
        


    }


    
}
