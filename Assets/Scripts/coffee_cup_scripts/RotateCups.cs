using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCups : MonoBehaviour
{
    public float delta = 60;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {          
        transform.Rotate(0,-delta*Time.deltaTime,0);
    }
}
