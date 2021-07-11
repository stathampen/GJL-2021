using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private Transform playerModel;

    public float shipSpeed = 18;
    public float rotationSpeed = 340;
    public float forwardSpeed = 6;

    public Transform aimTarget;

    public bool joystick = true;

    public CinemachineDollyCart dollyCart;

    // Start is called before the first frame update
    void Start()
    {
        setSpeed(forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(h, v, shipSpeed);
        RotationLook(h, v, rotationSpeed);
        HorizontalLean(playerModel, h, 80, .1f);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }

    private void setSpeed(float x)
    {
        dollyCart.m_Speed = x;
    }
}
