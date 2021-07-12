using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform debugHitTransform;

    public State state;

    public float pullbackSpeedMultiplier = 4f;

    [Space]

    [Header("Bullet Shooty Variables")]
    public GameObject BulletPrefab;
    public Transform BulletOrigin;
    public float shootForce;

    private Transform latchedAmmo;
    private int ammoLayer;

    public enum State {
        Idle,   //nothing special
        PullBack,   //the player is pulling back ammo
        Loaded  //the player has ammo
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        ammoLayer = LayerMask.GetMask("Ammo");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            
            case State.PullBack:
                HandleHookShotPullBack();
                break;

            case State.Loaded:
                HandleShoot();
            break;

            default:
            case State.Idle:
                HandleHookShotStart();
                break;
        }
    }

    private void HandleHookShotStart()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //get where the mouse is on the screen as we want to fire in that direction
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100f, ammoLayer))
            {
                //now do this
                debugHitTransform.position = hit.point;
                
                //store the position of the latched object
                latchedAmmo = hit.transform;

                state = State.PullBack;
            }
        }
    }

    private void HandleHookShotPullBack()
    {
        //start to pull back the ammo to the player

        float pullbackSpeedMin = 10f;
        float pullbackSpeedMax = 40f;

        //have it faster the further away the ammo is and slows down when it gets close
        float pullbackSpeed = Mathf.Clamp(
            Vector3.Distance(transform.position, latchedAmmo.position),
            pullbackSpeedMin,
            pullbackSpeedMax
        );

        latchedAmmo.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //if the player grabs a bullet we need to cancel its destroy timer
        if(latchedAmmo.GetComponent<BulletBehaviour>())
        {
            if(latchedAmmo.GetComponent<BulletBehaviour>().willSelfDestruct)
                latchedAmmo.GetComponent<BulletBehaviour>().StopSelfDestruct();
        }

        latchedAmmo.position = Vector3.MoveTowards(latchedAmmo.position, transform.position, pullbackSpeed * pullbackSpeedMultiplier * Time.deltaTime);

        float equipAmmoDistance = 2f;
        if (Vector3.Distance(transform.position, latchedAmmo.position) < equipAmmoDistance)
        {
            Destroy(latchedAmmo.gameObject);
            //ammo is equipped -- you can now shoot!
            state = State.Loaded;
        }
    }

    private void HandleShoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //shoot the rubble!!
            GameObject newBullet = (GameObject)Instantiate(
                BulletPrefab,
                BulletOrigin.position,
                Quaternion.Euler(ray.direction)
            );

            newBullet.GetComponent<Rigidbody>().AddForce(ray.direction * shootForce);

            state = State.Idle;
        }

    }

}
