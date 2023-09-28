using UnityEngine;

public class AnswerCube : MonoBehaviour
{
    // QuestionnaireManager script
    [Tooltip("QuestionnaireManager script")]
    [SerializeField]
    private QuestionnaireManager questionnaireManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("laser"))
        {
            questionnaireManager.questionAnswers.Add(gameObject.name);
            questionnaireManager.NextQuestion();
        }
    }
}