using UnityEngine;

/// <summary>
/// Laser controller
/// </summary>
/// 


public class Laser : MonoBehaviour
{
    private logicFunctions logic;
    private MaterialChanger materialChanger;
    private static bool isSolved = false;


    #region - Start/Update -
    /// <summary>
    /// Called before the first frame update
    /// </summary>
    /// 


    void Start()
    {
        Debug.Log(124124);
        transform.forward = Camera.main.transform.position - transform.position;
        logic= GameObject.Find("Logic").GetComponent<logicFunctions>();
        materialChanger = GameObject.Find("cube1").GetComponent<MaterialChanger>();
        
    }
   

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // Destroy laser after a few seconds
        Destroy();
    }
    #endregion

    #region - Other Methods -

    /// <summary>
    /// // Destroy laser after a few seconds
    /// </summary>
    /// 

    

    private void Destroy()
    {
        Destroy(gameObject, 5f);
    }

    /// <summary>
    /// Called when the collider enters another trigger
    /// </summary>
    /// <param name="other">Other trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        
        if (!isSolved)
        {
            switch (other.gameObject.name)
            {
                case "Pillar1":

                    logic.IncrementPillar("Pillar1");
                    break;
                case "Pillar2":

                    logic.IncrementPillar("Pillar2");
                    break;
                case "Pillar3":

                    logic.IncrementPillar("Pillar3");
                    break;

            }

            isSolved = logic.CheckPuzzle();
            materialChanger.ChangeMaterial(logic.PillarList);
        }


        Destroy(gameObject);
    }
    #endregion
}
