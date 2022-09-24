using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float x = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Time.deltaTime * 10f;

        if(Input.GetKey(KeyCode.Z)){
            transform.Rotate(0,0,15.0f*Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.R)){
            transform.Rotate(0,0,-15.0f*Time.deltaTime);
        }
        
    }
}
