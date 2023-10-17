using TMPro;
using UnityEngine;

public class EnergyPillarLogic : MonoBehaviour
{
    [SerializeField] Light doorLight;
    [SerializeField] GameObject door;
    public TextMeshProUGUI messageText;
    public string messageContent = "Puzzle cleared! The door has opened";

    // First questionnaire question object
    [Tooltip("First questionnaire question object")]
    [SerializeField]
    private GameObject firstQuestion;

    // Help text object on the wall
    [Tooltip("Help text object on the wall")]
    [SerializeField]
    private GameObject helpText;

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

    // Timer script reference
    [Tooltip("Timer script reference")]
    [SerializeField]
    private Timer timer;

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
            helpText.SetActive(false);
            firstQuestion.SetActive(true);
            playerShooting.SetIsSolved(true);
            timer.PauseTimer();
        }
        return isSolved;
    }
}
