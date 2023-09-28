using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionnaireManager : MonoBehaviour
{
    // List of questionnaire question objects
    [Tooltip("List of questionnaire question objects")]
    [SerializeField]
    private List<GameObject> questionnaireQuestions;

    // GameEnded text object
    [Tooltip("GameEnded text object")]
    [SerializeField]
    private GameObject gameEnded;


    [HideInInspector]
    public List<string> questionAnswers = new();

    private string currentLevel = "1";

    private int currentQuestionIndex = 0;

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
    public void SaveToCSV()
    {
        TextWriter tw = new StreamWriter("player_data.csv", false);
        tw.WriteLine("Level; Frustration; Difficulty");
        tw.Close();

        tw = new StreamWriter("player_data.csv", true);
        tw.WriteLine(currentLevel + ";" + questionAnswers[0] + ";" + questionAnswers[1]);
        tw.Close();
    }

}