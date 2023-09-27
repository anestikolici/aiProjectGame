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

    private logicFunctions logic;


    void Start()
    {
        //lights = GameObject.FindGameObjectsWithTag("lights"); //collect all cubes
        //index = Random.Range(0, lights.Length); //specify some random cubes to be turned on at the start
        //selectedcube = lights[index];
        //print(selectedcube.name);
        //selectedcube.GetComponent<MeshRenderer>().material= lightOn; //opening randomly some lights at the begining of the game
        //parentGameObject = this.gameObject.transform.parent.gameObject;
        cubes_1 = GameObject.FindGameObjectsWithTag("lights1");
        cubes_2 = GameObject.FindGameObjectsWithTag("lights2");
        cubes_3 = GameObject.FindGameObjectsWithTag("lights3");

        logic = GameObject.Find("Logic").GetComponent<logicFunctions>();


        ChangeMaterial(logic.PillarList);


    }

   
    public void ChangeMaterial(Dictionary<string, int> PillarList )
    {

        
        //First pillar
        foreach (GameObject cube in cubes_1)
        {

            cube.GetComponent<MeshRenderer>().sharedMaterial = lightOff;
            
        }
        Debug.Log("check");

        for (int i=0;i<3;i++)
        {
            if (PillarList["Pillar1"]<=i) { continue; }

            print(PillarList["Pillar1"]);
            
            if (cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial == lightOff)
            {
                cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial = lightOn;
            }

            else
            {
                cubes_1[i].GetComponent<MeshRenderer>().sharedMaterial = lightOff;
            }


        }

        //Second pillar
        foreach (GameObject cube in cubes_2)
        {

            cube.GetComponent<MeshRenderer>().sharedMaterial = lightOff;

        }


        for (int i = 0; i < 3; i++)
        {
            if (PillarList["Pillar2"] <= i) { continue; }

            print(PillarList["Pillar2"]);

            cubes_2[i].GetComponent<MeshRenderer>().sharedMaterial = lightOn;

            Debug.Log("test pillar 2");

        }

        //Third pillar
        foreach (GameObject cube in cubes_3)
        {

            cube.GetComponent<MeshRenderer>().sharedMaterial = lightOff;

        }


        for (int i = 0; i < 3; i++)
        {
            if (PillarList["Pillar3"] <= i) { continue; }

            print(PillarList["Pillar3"]);

            cubes_3[i].GetComponent<MeshRenderer>().sharedMaterial = lightOn;


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

        
        


    }


    
}
