using TMPro;
using UnityEngine;

public class EnergyPillarLogic : MonoBehaviour
{
    [SerializeField] Light doorLight;
    [SerializeField] GameObject door;
    public TextMeshProUGUI messageText;
    public string successContent = "Puzzle cleared! The door has opened";
    public string failureContent = "Time has run out! The door has opened";

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

    private bool isSolved = false;

    public void CheckPuzzle()
    {
        isSolved = true;
        foreach (EnergyBar energyBar in pillar3.GetEnergyBars())
        {
            if (!energyBar.isActiveAndEnabled)
            {
                isSolved = false;
                break;
            }
        }

        if (isSolved)
            EndLevel(true);
    }

    public void EndLevel(bool success)
    {
        isSolved = true;
        doorLight.color = Color.green;
        door.GetComponent<DoorRotation>().enabled = true;
        if (success)
            messageText.text = successContent;
        else
            messageText.text = failureContent;
        messageText.enabled = true;
        audioPlayerCleared.PlayAudio();
        helpText.SetActive(false);
        firstQuestion.SetActive(true);
        playerShooting.SetIsSolved(true);
        timer.PauseTimer();
    }

    public bool GetIsSolved()
    {
        return isSolved;
    }
}
