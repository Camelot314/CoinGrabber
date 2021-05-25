using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private static int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Score: " + score;
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
}
