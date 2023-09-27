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
    GameObject[] cubes_1;
    GameObject[] cubes_2;
    GameObject[] cubes_3;



    void Start()
    {
        //lights = GameObject.FindGameObjectsWithTag("lights"); //collect all cubes
        //index = Random.Range(0, lights.Length); //specify some random cubes to be turned on at the start
        //selectedcube = lights[index];
        //print(selectedcube.name);
        //selectedcube.GetComponent<MeshRenderer>().material= lightOn; //opening randomly some lights at the begining of the game
        //parentGameObject = this.gameObject.transform.parent.gameObject;
        cubes_1 = GameObject.FindGameObjectsWithTag("lights2");
        print(cubes_1);

       


    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

       

        for (int i=0;i<3;i++)
        {

            print(cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial.name);
            if (cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial == lightOff)
            {
                cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial = lightOn;
            }

            else
            {
                cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial = lightOff;
            }
        }

        //not deleting it because i want it as reference
        /*    foreach (GameObject light in lights)
         {
             if (light.GetComponent<MeshRebnderer>().sharedMaterial == lightOff)
             {
                 light.GetComponent<MeshRenderer>().sharedMaterial = lightOn;
             }

             else
             {
                light.GetComponent<MeshRenderer>().sharedMaterial = lightOff;
             }
         }

         /*if (GetComponent<MeshRenderer>().sharedMaterial == lightOff)
         {
             GetComponent<MeshRenderer>().sharedMaterial = lightOn;

         }

         else
         {
             GetComponent<MeshRenderer>().sharedMaterial = lightOff;
         }*/

        hitCounts++;
        


    }


    
}
