using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour
{
    public float selfDestructTimer = 5f;

    public float speed = 15f;

    public Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySelfTimer", selfDestructTimer);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = new Vector3(
            transform.position.x - moveVector.x * step,
            transform.position.y - moveVector.y * step,
            transform.position.z - moveVector.z * step
        );
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
    
    IEnumerator DestroySelfTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // public IEnumerator Intercept(Vector3 moveVector)
    // {
    //     Debug.Log("aaah");

    //     float step = speed * Time.deltaTime;

    //     transform.position = new Vector3(
    //         transform.position.x - moveVector.x * step,
    //         transform.position.x - moveVector.x * step,
    //         transform.position.x - moveVector.x * step
    //     );

    //     yield return new WaitForSeconds(1);
    // }

}
