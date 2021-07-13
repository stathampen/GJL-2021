using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    private Transform target;
    public float range = 15f;
    public float rotationSpeed = 10f;
    public string playerTag = "Player";

    public Transform partToRotate;

    // Start is called before the first frame update
    void Start()
    {
        //get the player object
        target = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void Update() 
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToPlayer < range)
        {
            
            //look at the player when they're close enough
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Vector3 rotation = lookRotation.eulerAngles;
            
            //rotate the whole thing! nice a smooth of course
            partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed) ;

            //We can shoot the player!


        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
