using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EnergyPillarLogic : MonoBehaviour
{
    [SerializeField] Light doorLight;
    [SerializeField] GameObject door;
    public TextMeshProUGUI messageText;
    public string messageContent = "Puzzle cleared! The door has opened";

    // List of questionnaire question objects
    [Tooltip("List of questionnaire question objects")]
    [SerializeField]
    private List<GameObject> questionnaireQuestions;

    // AudioPlayerCleared script
    [Tooltip("AudioPlayerCleared script")]
    [SerializeField]
    private AudioPlayerCleared audioPlayerCleared;

    // Player shooting script
    [Tooltip("Player shooting script")]
    [SerializeField]
    private PlayerShooting playerShooting;

    // Pillar 3
    [Tooltip("Pillar 3")]
    [SerializeField]
    private EnergyPillar pillar3;

    public bool CheckPuzzle()
    {
        bool isSolved = true;
        foreach (EnergyBar energyBar in pillar3.GetEnergyBars())
        {
            if (!energyBar.isActiveAndEnabled)
            {
                isSolved = false;
                break;
            }
                
        }

        if (isSolved)
        {
            doorLight.color = Color.green;
            door.GetComponent<DoorRotation>().enabled = true;
            messageText.text = messageContent;
            messageText.enabled = true;
            audioPlayerCleared.PlayAudio();
            questionnaireQuestions[0].SetActive(true);
            playerShooting.SetIsSolved(true);
        }
        return isSolved;
    }
}
