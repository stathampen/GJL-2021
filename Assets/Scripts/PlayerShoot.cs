using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool hasAmmo = false;

    public Transform debugHitTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleHookShotStart();
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
            }
        }
    }

}
