using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private logicFunctions logic;
    private MaterialChanger materialChanger;
    private static bool isSolved = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("Logic").GetComponent<logicFunctions>();
        materialChanger = GameObject.Find("cube1").GetComponent<MaterialChanger>();
    }

    /// <summary>
    /// Called when the collider enters another trigger
    /// </summary>
    /// <param name="other">Other trigger</param>
    private void OnTriggerEnter(Collider other)
    {

        if (!isSolved && other.CompareTag("laser"))
        {
            logic.IncrementPillar(gameObject.name);
            isSolved = logic.CheckPuzzle();
            materialChanger.ChangeMaterial(logic.PillarList);
        }
    }
}
