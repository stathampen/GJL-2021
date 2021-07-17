using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float selfDestructTimer = 5f;
    public bool willSelfDestruct;

    // Start is called before the first frame update
    void Start()
    {
        willSelfDestruct = true;
        StartCoroutine("DestroySelfTimer", selfDestructTimer);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
        //if it otherwise hits the player...
    }

    public void StopSelfDestruct()
    {
        willSelfDestruct = false;
        StopCoroutine("DestroySelfTimer");
    }

    IEnumerator DestroySelfTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(willSelfDestruct)
        {
            //just in case
            Destroy(gameObject);
            
        }
    }
}
