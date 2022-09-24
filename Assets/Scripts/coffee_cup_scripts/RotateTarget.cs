using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateTarget : MonoBehaviour
{
    [Header("大円盤の回転数（degree/sec）")]
    public float rotAng2 = 36;

    void Update () {
         transform.Rotate(new Vector3(0.0f, rotAng2*Time.deltaTime,0.0f));
         //一秒間に約３度回転している
    }
}


