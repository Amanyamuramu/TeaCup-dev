using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_base : MonoBehaviour
{

    [Header("大円盤の回転数（degree/sec）")]
    public float rotAng = 24;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, -rotAng*Time.deltaTime,0.0f));
        //一秒間に約３度回転している
         //1.6度数は10になる
    }
}
