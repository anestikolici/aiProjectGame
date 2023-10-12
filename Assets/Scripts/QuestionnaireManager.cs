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
    [Tooltip("Reference to the Timer script")]
    [SerializeField]
    private Timer timer;


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
            gameEnded.SetActive(true);
            SaveToCSV();
        }
    }

    /// <summary>
    /// Saves player data to a CSV file
    /// </summary>
    public void SaveToCSV()
    {
        TextWriter tw = new StreamWriter("player_data.csv", false);
        tw.WriteLine("Level; Valence; Arousal; Dominance; Bullets Shot; Number of Resets; Elapsed Time");
        tw.Close();

        tw = new StreamWriter("player_data.csv", true);
        switch (currentLevel)
        {
            case "1":
                tw.WriteLine(currentLevel + ";" + questionAnswers[0] + ";" + questionAnswers[1] + ";" + questionAnswers[2] + ";" + playerShooting.GetBulletsShot() + ";_; " + timer.ElapsedTime);
                break;
            case "2":
                tw.WriteLine(currentLevel + ";" + questionAnswers[0] + ";" + questionAnswers[1] + ";" + questionAnswers[2] + ";_" + ";" + resetTile.GetTotalResets() + ";" + timer.ElapsedTime);
                break;
            case "3":
                tw.WriteLine(currentLevel + ";" + questionAnswers[0] + ";" + questionAnswers[1] + ";_;" + playerShooting.GetBulletsShot() + ";" + pillar.GetTotalResets() + ";" + + timer.ElapsedTime);
                break;
        }

        tw.Close();
    }

    #endregion
}