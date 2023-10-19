using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    // Level 1 logic script reference
    [Tooltip("Level 1 logic script reference")]
    [SerializeField]
    private logicFunctions logicFunction1;

    // Level 2 logic script reference
    [Tooltip("Level 2 logic script reference")]
    [SerializeField]
    private TilePuzzleLogic logicFunction2;

    // Level 3 logic script reference
    [Tooltip("Level 3 logic script reference")]
    [SerializeField]
    private EnergyPillarLogic logicFunction3;

    public TMP_Text timerText;
    private float elapsedTime = 300f;
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
            elapsedTime -= Time.deltaTime;
            if (elapsedTime < 0)
            {
                if (logicFunction1 != null)
                    logicFunction1.EndLevel(false);
                else if (logicFunction2 != null)
                    logicFunction2.EndLevel(false);
                else if (logicFunction3 != null)
                    logicFunction3.EndLevel(false);
                elapsedTime = 0f;
                isTimerRunning = false;
            }
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (timerText != null)
            timerText.text = "Time: " + timerString;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 300f;
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }
}
