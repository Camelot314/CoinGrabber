using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{
    [SerializeField] private static int score;
    private static long startTime, duration, currentTime, prevDuration;
    private static bool timing;
    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timing)
        {
            currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            duration = currentTime - startTime + prevDuration;
        }
        TimeSpan timeFormatter = TimeSpan.FromMilliseconds(duration);
        string elapsed = string.Format("{1:D2}:{2:D2}:{3:D3}",
                timeFormatter.Hours,
                timeFormatter.Minutes,
                timeFormatter.Seconds,
                timeFormatter.Milliseconds);
        string toWrite = "Score: " + score;
        GetComponent<Text>().text = "Score: " + score + "\n" + elapsed;
    }

    // This will increase the score by one. 
    public static void IncreaseScore()
    {
        score ++;
    }

    // This will increase the score by a set value.
    public static void IncreaseScore(int amount)
    {
        score += amount;
    }

    // this will reset the score to 0.
    public static void ResetScore()
    {
        score = 0;
    }

    // This will get the current scor.e 
    public static int GetScore()
    {
        return score;
    }

    public static void StopTimer()
    {
        prevDuration = duration;
        timing = false;
    }

    public static void ResetDisplay()
    {
        prevDuration = 0;
        duration = 0;
        score = 0;
    }

    public static void StartTimer()
    {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        currentTime = startTime;
        timing = true;
    }
}
