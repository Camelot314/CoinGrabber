using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject finalCoinPrefab, coinPreFab, 
        player, score, popUpMessage, coinHolderObject, resumeButton,
        tryAgainButton;
    private static float ERROR_MARGIN = 0.0001f;
    private Vector3 finalCoinPos;
    private Vector3[] coinPos;
    private Quaternion coinRotation;
    private Player playerScript;
    private PopUp popUpScript;
    private bool paused, pausedPressed, gameOver;
   


    // Start is called before the first frame update
    void Start()
    {
        
        GameObject finalCoin = GameObject.FindGameObjectWithTag("FinalCoin");
        finalCoinPos = finalCoin.GetComponent<Transform>().position;
        coinRotation = finalCoin.transform.rotation;
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        coinPos = new Vector3[coins.Length];
        Transform coinTransform;

        for (int i = 0; i < coins.Length; i ++)
        {
            coinTransform = coins[i].GetComponent<Transform>();
            coinPos[i] = coinTransform.position;
        }
        playerScript = player.GetComponent<Player>();
        popUpScript = popUpMessage.GetComponent<PopUp>();
        Pause("Menu", "Start", false);
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Cancel")- 1) < ERROR_MARGIN)
        {
            if (gameOver || pausedPressed)
            {
                return;
            }
            pausedPressed = true;
            if (paused)
            {
                paused = false;
                Resume();
            } else
            {
                paused = true;
                Pause();
            }
        }
        if (Mathf.Abs(Input.GetAxis("Cancel")) < ERROR_MARGIN)
        {
            pausedPressed = false;
        }
    }

    /*
     * This will reset everything on the level. It will remove
     * any remaining coins. It will remake and respawn the coins.
     * It will reset the player the score counter and remove the 
     * pop up message.
     * */
    public void ResetLevel()
    {
        playerScript.ResetPlayer();
        if (popUpScript == null)
        {
            Debug.LogError("no refernce to pop up script");
        } else
        {
            popUpScript.ResetDisplay();
        }
        HUD.ResetDisplay();

        // Deleteing all the remaining coins
        GameObject[] remainingCoins = GameObject.FindGameObjectsWithTag("Coin");
        DeleteRemainingCoins(remainingCoins);

        remainingCoins = GameObject.FindGameObjectsWithTag("FinalCoin");
        DeleteRemainingCoins(remainingCoins);

        GameObject temp;
        for (int i = 0; i < coinPos.Length; i ++)
        {
            temp = Instantiate(coinPreFab, coinPos[i] , coinRotation);
            temp.tag = "Coin";
            temp.transform.parent = coinHolderObject.transform;
        }
        temp = Instantiate(finalCoinPrefab, finalCoinPos, coinRotation);
        temp.tag = "FinalCoin";
        temp.transform.parent = coinHolderObject.transform;
        gameOver = false;
        HUD.StartTimer();
    }

    public void EndGame(bool win)
    {
        if (gameOver)
        {
            return;
        }
        if (popUpScript != null)
        {
            popUpScript.GameOver(win);
        }
        gameOver = true;
        HUD.StopTimer();
        resumeButton.SetActive(false);
        tryAgainButton.GetComponent<Button>().SetTryAgainText("Try Again");
    }

    // pauses the game. Makes the player ignore control input
    // makes the player stationary. Displays the paused message.
    // stops the timer.
    private void Pause()
    {
        Pause("Paused", "Try Again", true);
    }

    // pauses the game. Makes the player ignore control input
    // makes the player stationary. Displays the paused message.
    // stops the timer.
    private void Pause(string text, string buttonText, bool showResume)
    {
        HUD.StopTimer();
        playerScript.SetIgnoreControl(true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        popUpScript.DisplayText(text);
        
        tryAgainButton.GetComponent<Button>().SetTryAgainText(buttonText);
        resumeButton.SetActive(showResume);
    }

    // resumes the game. The player no longer ignores input. The
    // player is no longer stationary. The timer starts again. 
    public void Resume()
    {
        playerScript.SetIgnoreControl(false);
        player.GetComponent<Rigidbody>().isKinematic = false;
        popUpScript.ResetDisplay();
        HUD.StartTimer();
        resumeButton.SetActive(false);
        paused = false;
    }

    private void DeleteRemainingCoins(GameObject[] list)
    {
        if (list.Length > 0)
        {
            foreach (GameObject coin in list)
            {
                Destroy(coin);
            }
        }
    }
}
