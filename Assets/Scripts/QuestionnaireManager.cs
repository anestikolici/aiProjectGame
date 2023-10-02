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
    [Tooltip("Player shooting script")]
    [SerializeField]
    private PlayerShooting playerShooting;

    #endregion

    #region - Non Serialize Fields -

    // List containing the answers to the questionnaire questions
    [HideInInspector]
    public List<string> questionAnswers = new();

    // Current level number
    private string currentLevel = "1";

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
        tw.WriteLine("Level; Frustration; Difficulty; Bullets Shot");
        tw.Close();

        tw = new StreamWriter("player_data.csv", true);
        tw.WriteLine(currentLevel + ";" + questionAnswers[0] + ";" + questionAnswers[1] + ";" + playerShooting.GetBulletsShot());
        tw.Close();
    }

    #endregion
}