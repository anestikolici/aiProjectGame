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
    void Start()
    {
        transform.forward = Camera.main.transform.position - transform.position;         
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
        Destroy(gameObject, 4f);
    }
    #endregion
}
