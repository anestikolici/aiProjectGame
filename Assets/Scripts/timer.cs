using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime;
    private bool isTimerRunning = false;

    public float ElapsedTime
    {
        get { return elapsedTime; }
    }

    public bool IsTimerRunning()
    {
        return isTimerRunning;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = "Time: " + timerString;
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }
}
