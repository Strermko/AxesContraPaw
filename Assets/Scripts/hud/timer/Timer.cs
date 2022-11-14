using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeRemainingInSeconds = 10;
    private bool timerIsRunning = false;

    public TextMeshProUGUI timeText;
    public Canvas timerCanvas;
    
    public float TimeRemainingInSeconds
    {
        get => timeRemainingInSeconds;
        set => timeRemainingInSeconds = value;
    }

    public bool TimerIsRunning => timerIsRunning;

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemainingInSeconds > 0)
            {
                timeRemainingInSeconds -= Time.deltaTime;
                DisplayTime(timeRemainingInSeconds);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemainingInSeconds = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}