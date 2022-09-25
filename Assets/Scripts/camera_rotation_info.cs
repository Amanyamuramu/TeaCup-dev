using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_rotation_info : MonoBehaviour
{
    public Transform targetCamera;
    public Transform targetBase;
    void FixedUpdate () {
        Vector3 localAngle = targetCamera.transform.localEulerAngles;
        Vector3 localAngle2 = targetBase.transform.eulerAngles;
        // this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
        this.transform.rotation =  Quaternion.Euler(localAngle.x,localAngle2.y,localAngle.z);

    }
}

