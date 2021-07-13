using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour
{
    public float selfDestructTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySelfTimer", selfDestructTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
    
    IEnumerator DestroySelfTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
