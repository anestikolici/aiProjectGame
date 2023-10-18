using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionnaireManager : MonoBehaviour
{
    #region - Serialize Fields -

    // List of questionnaire question objects
    [Tooltip("List of questionnaire question objects")]
    [SerializeField]
    private List<GameObject> questionnaireQuestions;

    // GameEnded text object
    [Tooltip("GameEnded text object")]
    [SerializeField]
    private GameObject gameEnded;

    // Player shooting script
    [Tooltip("Player shooting script. Is only required in puzzles with shooting")]
    [SerializeField]
    private PlayerShooting playerShooting;

    // Reset tile
    [Tooltip("Reset tile. Is only required for the tile puzzle.")]
    [SerializeField]
    private Tile resetTile;

    // Pillar
    [Tooltip("Pillar. Any pillar will do. Is only required for the energy pillar puzzle.")]
    [SerializeField]
    private EnergyPillar pillar;

    // Current level number
    [Tooltip("Current level number")]
    [SerializeField]
    private string currentLevel = "1";

    // timer
    [Tooltip("Timer script reference")]
    [SerializeField]
    private Timer timer;

    [Tooltip("DoorRotation script")]
    [SerializeField]
    private DoorRotation doorRotation;

    [Tooltip("HintManager script")]
    [SerializeField]
    private HintManager hintManager;

    #endregion

    #region - Non Serialize Fields -

    // List containing the answers to the questionnaire questions
    [HideInInspector]
    public List<string> questionAnswers = new();

    // Index of the current questionnaire question
    private int currentQuestionIndex = 0;

    #endregion

    #region - Other Methods -
    /// <summary>
    /// De-activates the current questionnaire question and activate the next one, if it exists. Otherwise, it saves the player data to a CSV file
    /// </summary>
    public void NextQuestion()
    {
        if (currentQuestionIndex < questionnaireQuestions.Count - 1)
        {        
            questionnaireQuestions[currentQuestionIndex].SetActive(false);
            currentQuestionIndex += 1;
            questionnaireQuestions[currentQuestionIndex].SetActive(true);
        }
        else
        {
            questionnaireQuestions[currentQuestionIndex].SetActive(false);
            doorRotation.SetIsAnswered(true);
            gameEnded.SetActive(true);
            SaveToCSV();
        }
    }

    /// <summary>
    /// Saves player data to a CSV file
    /// </summary>
    public void SaveToCSV()
    {
        StreamWriter sw = new("player_data.csv", true);

        if (sw.BaseStream.Length == 0)
        {
            sw.WriteLine("Level;Valence;Arousal;Dominance;Bullets Shot/Stepped Tiles;Number of Resets;HintsPressed;Elapsed Time");
        }

        string line;

        switch (currentLevel)
        {
            case "1":
                line = $"{currentLevel};{questionAnswers[0]};{questionAnswers[1]};{questionAnswers[2]};{playerShooting.GetBulletsShot()};0;0;{timer.ElapsedTime}";
                sw.WriteLine(line);              
                break;
            case "2":
                line = $"{currentLevel};{questionAnswers[0]};{questionAnswers[1]};{questionAnswers[2]};{resetTile.GetTotalTiles()};{resetTile.GetTotalResets()};{hintManager.GetHitnsPressed()};{timer.ElapsedTime}";
                sw.WriteLine(line);
                break;
            case "3":
                line = $"{currentLevel};{questionAnswers[0]};{questionAnswers[1]};{questionAnswers[2]};{playerShooting.GetBulletsShot()};{pillar.GetTotalResets()};{hintManager.GetHitnsPressed()};{timer.ElapsedTime}";
                sw.WriteLine(line);
                break;
        }
        sw.Close();
    }

    #endregion
}