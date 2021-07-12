using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private Transform playerModel;

    public float shipSpeed = 18;
    public float rotationSpeed = 340;
    public float forwardSpeed = 6;

    public Transform aimTarget;

    public bool joystick = true;

    [Header("Action Bools")]
    public bool isBoosting = false;
    public bool isBraking = false;

    public CinemachineDollyCart dollyCart;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, shipSpeed);
        RotationLook(h, v, rotationSpeed);
        HorizontalLean(playerModel, h, 80, .1f);

        //BOOST FOR SPEEEEEEEEEEEEED
        if (Input.GetButtonDown("Fire3"))
            Boost(true);

        if (Input.GetButtonUp("Fire3"))
            Boost(false);


        //Gotta take is slow my due
        if (Input.GetButtonDown("Jump"))
            Brake(true);

        if (Input.GetButtonUp("Jump"))
            Brake(false);

    }

    void LocalMove(float x, float y, float speed)
    {
        //just move th eship in its local space as movement forward will be handled elswhere
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();

    }

    void ClampPosition()
    {
        //stops the player going flying off into the void
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    void Boost(bool state)
    {
        isBoosting = state;

        //need to tell camera to look fancy at this point?
        if (state)
        {
            //whilst boosting do this
            //put some fancy trails here
        }
        else
        {
            //do this instead
        }

        //if state is true times forward speed by 2, otherwise do not
        float speed = state ? forwardSpeed * 2 : forwardSpeed;

        DOVirtual.Float(dollyCart.m_Speed, speed, .15f, SetSpeed);
    }

    void Brake(bool state)
    {
        isBraking = state;

        //need to tell camera to look fancy at this point?
        if (state)
        {
            //whilst braking do this
            //put some fancy trails here
        }
        else
        {
            //do this instead
        }

        //if state is true times forward speed by 2, otherwise do not
        float speed = state ? forwardSpeed / 2 : forwardSpeed;

        DOVirtual.Float(dollyCart.m_Speed, speed, .15f, SetSpeed);
    }
    void SetSpeed(float x)
    {
        dollyCart.m_Speed = x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }
}
