using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierTrigger : MonoBehaviour
{
    public int levelCameraNo;
    public CameraBrain cameraBrain;

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Tansition time");
        cameraBrain.setLevelCamera(levelCameraNo);
    }
}
