using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{

    public GameObject endGameUI;
    public Text endGameText;

    public string winText;
    public string loseText;

    public void ShowEndGameUI(bool hasPlayerWon)
    {
        //set the game object to enabled
        endGameUI.SetActive(true);

        //stop the game time
        Time.timeScale = 0;

        if(hasPlayerWon)
        {
            endGameText.text = winText;
        }
        else
        {
            endGameText.text = loseText;
        }
    }
}
