using UnityEngine;

/// <summary>
/// Laser controller
/// </summary>
/// 


public class Laser : MonoBehaviour
{



    #region - Start/Update -
    /// <summary>
    /// Called before the first frame update
    /// </summary>
    /// 
    private logicFunctions logic;

    void Start()
    {
        transform.forward = Camera.main.transform.position - transform.position;
        logic= GameObject.Find("Logic").GetComponent<logicFunctions>();

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

               logic.CheckPuzzle();

        Destroy(gameObject);
    }
    #endregion
}
