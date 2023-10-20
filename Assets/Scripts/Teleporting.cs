using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Timer timer;

    [Tooltip("Level 1 logic script reference")]
    [SerializeField]
    private logicFunctions logicFunction;

    [Tooltip("Player Transform")]
    [SerializeField]
    private Transform player;

    private bool hasTeleported = false;


    void Update()
    {
       if(logicFunction != null && timer.elapsedTime == 0f && !hasTeleported)
       {
            Teleport();
            hasTeleported = true;
       }
    }

    void Teleport()
    {
        player.transform.position = new Vector3(23f, 18f, 72f);
    }

   
}
