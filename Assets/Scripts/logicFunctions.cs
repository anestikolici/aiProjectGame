using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class logicFunctions : MonoBehaviour
{
    private Dictionary<string, int> pillarList;

    private MaterialChanger materialChanger;

    [SerializeField] Light doorLight;

    public Dictionary<string, int> PillarList
    {
        get { return pillarList; }
    }
    private GameObject[] pillarKey;
   
   

    // Start is called before the first frame update
    void Start()
    {
        pillarList = new Dictionary<string, int>();
        pillarKey = GameObject.FindGameObjectsWithTag("pillar");

        materialChanger = GameObject.Find("cube1").GetComponent<MaterialChanger>();


        LoadPillar();

        //loads lights

    }

    public void LoadPillar()
    {
        int rnd;
        for (int i = 0; i < 3; i++)
        {

            pillarList.Add(pillarKey[i].name, rnd = UnityEngine.Random.Range(1, 3)) ;


            //Debug.Log("asdasd"+pillarList);

        }

        foreach (KeyValuePair<string, int> kvp in pillarList)
            Debug.Log( kvp.Key + "/" + kvp.Value);





    }

    //increments the number for the puzzle from 1 to 3
    public void IncrementPillar(string pillar)
    {
        int incr, previous, next;

        switch (pillar)
        {
            case "Pillar1":
                incr = pillarList["Pillar1"] + 1;
                next = pillarList["Pillar2"] + 1;

                pillarList["Pillar1"] = IncrCheck(incr);
                pillarList["Pillar2"] = IncrCheck(next);
                Debug.Log(pillarList["Pillar1"] + "   " + pillarList["Pillar2"] + "   " + pillarList["Pillar3"]);
                break;
            case "Pillar2":
                incr = pillarList["Pillar2"] + 1;

                previous = pillarList["Pillar1"] + 1;
                next = pillarList["Pillar3"] + 1;

                pillarList["Pillar2"] = IncrCheck(incr);
                pillarList["Pillar1"] = IncrCheck(previous);
                pillarList["Pillar3"] = IncrCheck(next);

                Debug.Log(pillarList["Pillar1"] + "   " + pillarList["Pillar2"] + "   " + pillarList["Pillar3"]);
                break;
            case "Pillar3":
                incr = pillarList["Pillar3"] + 1;
                previous = pillarList["Pillar2"] + 1;

                pillarList["Pillar3"] = IncrCheck(incr);
                pillarList["Pillar2"] = IncrCheck(previous);

                Debug.Log(pillarList["Pillar1"] + "   " + pillarList["Pillar2"] + "   "+ pillarList["Pillar3"]);
                break;
        }


    }

    public int IncrCheck(int incr)
    {
        if (incr > 3)
        {
            incr = 1;
            
        }
        return incr;
    }

    public void CheckPuzzle()
    {

        int temp = pillarList.ElementAt(0).Value;

        int count=0;

        foreach(KeyValuePair<string, int> kvp in pillarList)
        {
            if(kvp.Value == temp)
            {
                count++;
                if (count == 3)
                {
                    //put door opening
                    doorLight.color = Color.green;
                    Debug.Log("SOLVED");

                }

            }
        }

    }



    


}
