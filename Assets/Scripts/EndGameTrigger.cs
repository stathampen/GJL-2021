using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public EndGameController endGameController;

    private void OnTriggerEnter(Collider other) 
    {
        endGameController.ShowEndGameUI(true);
    }
}
