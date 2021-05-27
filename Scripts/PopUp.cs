using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject textField, panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // will set displaying the message to true and display
    // you win if win is true. 
    public void GameOver(bool win)
    {
        panel = gameObject;
        if (panel != null)
        {
            panel.SetActive(true);
            DisplayText(win);
        }
    }

    public void ResetDisplay()
    {
        panel.SetActive(false);
        textField.SetActive(false);
    }

    private void DisplayText(bool win)
    {
        if (textField == null)
        {
            Debug.Log("no text field");
            return;
        }
        textField.SetActive(false);
        if (win)
        {
            textField.GetComponent<Text>().text = "YOU WIN";
        }
        else
        {
            textField.GetComponent<Text>().text = "GAME OVER";
        }
        textField.SetActive(true);
    }

    public void DisplayText(string text)
    {
        textField.GetComponent<Text>().text = text;
        panel.SetActive(true);
        textField.SetActive(true);
    }


}
