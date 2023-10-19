using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform teleportPanel;
    public Transform player;
    public Timer timer;
    [Tooltip("Level 1 logic script reference")]
    [SerializeField]
    private logicFunctions logicFunction;



    void Update()
    {
       if(logicFunction != null && timer.elapsedTime == 0f)
       {
              Teleport();
       }
    }

    void Teleport()
    {
        Vector3 playerPosition = player.position;
        Vector3 targetPosition = teleportPanel.position;

        player.position = targetPosition;
    }

   
}
