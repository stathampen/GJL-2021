using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavour : MonoBehaviour
{
    // Start is called before the first frame update
    public int shipHealth = 10;

    public PlayerAduioController AduioController;

    public EndGameController endGameController;

    private void OnCollisionEnter(Collision other) {

        switch (other.gameObject.tag)
        {
            
            case "Ammo":
                //when the player collides with ammo pick it up
                gameObject.GetComponent<PlayerShoot>().state = PlayerShoot.State.Loaded;
                Destroy(other.gameObject);
            break;

            case "PlayerShot":
                //do nothing
            break;

            default:
                AduioController.PlayerHitSFX();
                RemoveHealth(1);
            break;
        }

        // if the player collides with the environment or prop take a little damage

        // if the player collides with ammo and they dont have any then pick it up
        

        // if the player collides with enemy shot then take some damage
    }

    private void RemoveHealth(int amount)
    {
        if(shipHealth > 0)
        {
            shipHealth -= amount;
        }
        else
        {

        }

    }
    private void AddHealth(int amount)
    {
        shipHealth += amount;
    }

    private void PlayerLose()
    {
        endGameController.ShowEndGameUI(false);
    }
}
