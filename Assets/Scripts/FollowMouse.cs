using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public RectTransform baseObject;
    public Vector3 offset;

    void Update()
    {
        Vector3 cursorPosition = Input.mousePosition + offset;

        transform.GetComponent<RectTransform>().position = cursorPosition;
    }
}
