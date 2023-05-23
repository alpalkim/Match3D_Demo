using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshPro TimeDisplay;
    float remainingTimeSeconds = 660;
    bool isCountdownActive = false;


    private void Update()
    {
        TimeDisplay.SetText(GetTimeWithMinutes(remainingTimeSeconds));
        if (isCountdownActive) 
            remainingTimeSeconds -= Time.deltaTime;
    }

    public void AddTime(float secondsAddition)
    {
        remainingTimeSeconds += secondsAddition;
    }
    

    public void SetTimer(float countdownSeconds)
    {
        remainingTimeSeconds = countdownSeconds;
    }

    public void StartTimer()
    {
        isCountdownActive = true;
    }

    public void ResetTimer()
    {
        isCountdownActive = false;
        remainingTimeSeconds = 0;
    }

    public void StopTimer()
    {
        isCountdownActive = false;
    }

    public float GetCurrentSeconds()
    {
        return remainingTimeSeconds;
    }


    public string GetTimeWithMinutes(float timeToDisplay)
    {

        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
