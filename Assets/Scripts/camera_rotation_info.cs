using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_rotation_info : MonoBehaviour
{
    public Transform targetCamera;
    void Update () {
        Vector3 localAngle = targetCamera.transform.localEulerAngles;
        // this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
        this.transform.rotation =  Quaternion.Euler(localAngle);

    }
}

