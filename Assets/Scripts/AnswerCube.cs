using UnityEngine;

public class AnswerCube : MonoBehaviour
{
    // QuestionnaireManager script
    [Tooltip("QuestionnaireManager script")]
    [SerializeField]
    private QuestionnaireManager questionnaireManager;

    /// <summary>
    /// Called when this object collides with another collider
    /// </summary>
    /// <param name="other">Other collider</param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("laser"))
        {
            questionnaireManager.questionAnswers.Add(gameObject.name);
            questionnaireManager.NextQuestion();
        }
    }
}