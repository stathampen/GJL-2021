using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public EndGameController endGameController;
    public PlayerAduioController AduioController;


    private void OnTriggerEnter(Collider other) 
    {
        endGameController.ShowEndGameUI(true);
        AduioController.PlayerWinMusic();
    }
}
