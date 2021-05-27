using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject level, tryAgainText, quitText, resumeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgain()
    {
        level.GetComponent<Level>().ResetLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        level.GetComponent<Level>().Resume();
    }

    public void SetTryAgainText(string text)
    {
        tryAgainText.GetComponent<Text>().text = text;
    }
}
