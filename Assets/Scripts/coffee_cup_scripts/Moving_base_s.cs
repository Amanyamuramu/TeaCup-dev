using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_base_s : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] Smallbase;

    public float rotAng = 360;

    // Start is called before the first frame update
    void Start()
    {
        Smallbase = GameObject.FindGameObjectsWithTag("Small");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, -rotAng*Time.deltaTime,0.0f));
        //一秒間に約３度回転している
         //1.6度数は10になる
    }
}
