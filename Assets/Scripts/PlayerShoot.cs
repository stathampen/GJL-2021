using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform debugHitTransform;

    public State state;

    public float pullbackSpeed = 4f;


    private Transform latchedAmmo;


    public enum State {
        Idle,   //nothing special
        PullBack,   //the player is pulling back ammo
        Loaded  //the player has ammo
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            
            default:
            case State.Idle:
                HandleHookShotStart();
                break;

            case State.PullBack:
                HandleHookShotPullBack();
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

            if(Physics.Raycast(ray, out hit))
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
        Vector3 hookShotDirection = (latchedAmmo.position - transform.position).normalized;
        
        //start to pull back the ammo to the player

        latchedAmmo.position = Vector3.MoveTowards(latchedAmmo.position, transform.position, pullbackSpeed * Time.deltaTime);
    }

}
