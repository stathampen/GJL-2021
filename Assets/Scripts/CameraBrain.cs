using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrain : MonoBehaviour
{

    [Header("Camera Rotations Per Level")]
    public Vector3 level1Rotation;
    public Vector3 level2Rotation;
    public Vector3 level3Rotation;
    public float rotationTime = 10f;

    public GameObject cameraHolderParent;
    public PlayerMovement playerMovement;
    public GameObject playerDisplayObject;

    // Start is called before the first frame update
    void Start()
    {
        setLevelCamera(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setLevelCamera(int level)
    {
        Debug.Log("Set camera: " + level);
        playerMovement.currentLevel = level;

        switch (level)
        {
            case 3:
                cameraHolderParent.transform.localEulerAngles  =  new Vector3(level3Rotation.x, level3Rotation.y, level3Rotation.z);
            break;

            case 2:
                cameraHolderParent.transform.localEulerAngles  =  new Vector3(level2Rotation.x, level2Rotation.y, level2Rotation.z);
            break;

            case 1:
                cameraHolderParent.transform.localEulerAngles  = new Vector3(level1Rotation.x, level1Rotation.y, level1Rotation.z);
            break;
        }

            playerDisplayObject.transform.localPosition = new Vector3(0, 0, 0);
            playerDisplayObject.transform.localEulerAngles = new Vector3(0, 0, 0);

    }

}
