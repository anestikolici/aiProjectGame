using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TilePuzzleLogic : MonoBehaviour
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

    // Array of all tiles
    private GameObject[] tiles;


    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public bool CheckPuzzle()
    {
        bool isSolved = true;
        foreach(GameObject tile in tiles)
        {         
            if (tile.GetComponent<Tile>().GetIsOn() == false)
            {
                isSolved = false;
                return isSolved;
            }
        }

        if (isSolved)
        {
            doorLight.color = Color.green;
            door.GetComponent<DoorRotation>().enabled = true;
            messageText.text = messageContent;
            messageText.enabled = true;
            Debug.Log("SOLVED");
            audioPlayerCleared.PlayAudio();
            questionnaireQuestions[0].SetActive(true);   
            weapon.SetActive(true);
            playerShooting.SetAmmo(30);
            ammoText.text = ("Ammo: 30");
            playerShooting.SetIsSolved(true);
        }
        return isSolved;
    }
}
