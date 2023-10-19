using TMPro;
using UnityEngine;

public class TilePuzzleLogic : MonoBehaviour
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

    // Weapon
    [Tooltip("Weapon")]
    [SerializeField]
    private GameObject weapon;

    // Player shooting script
    [Tooltip("Player shooting script")]
    [SerializeField]
    private PlayerShooting playerShooting;

    // Player ammo text
    [Tooltip("Ammo text")]
    [SerializeField]
    private TextMeshProUGUI ammoText;

    // Timer script reference
    [Tooltip("Timer script reference")]
    [SerializeField]
    private Timer timer;

    // Array of all tiles
    private GameObject[] tiles;

    private bool isSolved = false;


    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void CheckPuzzle()
    {
        isSolved = true;
        foreach (GameObject tile in tiles)
        {
            if (tile.GetComponent<Tile>().GetIsOn() == false)
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
        weapon.SetActive(true);
        playerShooting.SetAmmo(30);
        ammoText.text = ("Ammo: 30");
        playerShooting.SetIsSolved(true);
        timer.PauseTimer();
    }

    public bool GetIsSolved()
    {
        return isSolved;
    }
}
