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
    public float currentSpeed;

    public Transform aimTarget;

    public bool joystick = true;
    public float currentLevel = 1;

    [Header("Action Bools")]
    public bool isBoosting = false;
    public bool isBraking = false;

    public CinemachineDollyCart dollyCart;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(forwardSpeed);
        currentSpeed = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, shipSpeed);
        RotationLook(h, v, rotationSpeed);

        if(currentLevel == 2)
        {
            HorizontalLean(playerModel, v, 30, .1f);
        }
        else
        {
            HorizontalLean(playerModel, h, 60, .1f);
        }

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

        switch (currentLevel)
        {
            case 3:
                transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
            break;

            case 2:
                transform.localPosition += new Vector3(0, y, -x) * speed * Time.deltaTime;
            break;

            case 1:
                transform.localPosition += new Vector3(-x, y, 0) * speed * Time.deltaTime;
            break;
        }

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
        target.rotation = Quaternion.Euler(new Vector3(0,0,0));

        switch (currentLevel)
        {
            case 3:
                target.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
            break;

            case 2:
                target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngels.x, -axis * leanLimit, lerpTime), 0, 0);
            break;

            case 1:
                target.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(targetEulerAngels.z, axis * leanLimit, lerpTime));
            break;
        }

    }

    void Boost(bool state)
    {
        isBoosting = state;

        //if state is true times forward speed by 2, otherwise do not
        float speed = state ? forwardSpeed * 1.5f : forwardSpeed;

        currentSpeed = speed;

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

    public Vector3 NextPosition()
    {

        float currentPosition = dollyCart.m_Path.StandardizeUnit(dollyCart.m_Position, dollyCart.m_PositionUnits);
        Vector3 currentTransformPosition = dollyCart.m_Path.EvaluatePositionAtUnit(currentPosition, dollyCart.m_PositionUnits);  

        if(currentLevel == 2)
        {
            float futurePosition = dollyCart.m_Path.StandardizeUnit(dollyCart.m_Position + .2f,    dollyCart.m_PositionUnits);
            Vector3 furtureTransformPosition = dollyCart.m_Path.EvaluatePositionAtUnit(futurePosition, dollyCart.m_PositionUnits);  

            return Vector3.Normalize(currentTransformPosition - furtureTransformPosition);
        }
        else
        {
            float futurePosition = dollyCart.m_Path.StandardizeUnit(dollyCart.m_Position + .5f,    dollyCart.m_PositionUnits);
            Vector3 furtureTransformPosition = dollyCart.m_Path.EvaluatePositionAtUnit(futurePosition, dollyCart.m_PositionUnits);  

            return Vector3.Normalize(currentTransformPosition - furtureTransformPosition);
        }


    }
}
