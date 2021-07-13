using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    private Transform target;


    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f; //seconds
    private float fireCountDown = 0f;
    public float rotationSpeed = 10f;
    public float shootForce;

    [Header("Setup Fields")]
    public string playerTag = "Player";

    public Transform partToRotate;

    public GameObject LazerPrefab;
    public Transform LazerSpawn;

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
            if(fireCountDown <= 0f)
            {
                //root tooty point and shooty!
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;

        }
    }

    void Shoot()
    {
        GameObject newBullet = (GameObject)Instantiate(
            LazerPrefab,
            LazerSpawn.position,
            LazerSpawn.rotation
        );

        newBullet.GetComponent<Rigidbody>().AddForce(LazerSpawn.forward * shootForce);
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
