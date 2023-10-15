using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime;
    private bool isTimerRunning = false;

    public float ElapsedTime
    {
        get { return (float)Math.Round(elapsedTime, 3); }
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

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }
}
