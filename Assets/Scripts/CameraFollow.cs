using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 offset = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    public Vector2 limits = new Vector2(5, 3);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            transform.localPosition = offset;
        }


        // FollowTarget(target);
    }

    private void LateUpdate() {
        Vector3 localPos = transform.localPosition;

        //keep the camera a certain distance
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.y, limits.y), localPos.z);
    }

    public void FollowTarget(Transform target)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = target.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), ref velocity, smoothTime);
    }
}
