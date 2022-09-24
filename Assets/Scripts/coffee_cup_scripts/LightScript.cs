using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public GameObject[] slight = new GameObject[4];
    [Header("ライトの点滅切り替え間隔（sec）")]
    public float duration = 5.0f;
    Color[] color = new Color[4];
    Light[] lt = new Light[4]; 

    void Start()
    {
        for(int i = 0; i<slight.Length; i++){
            lt[i] = slight[i].GetComponent<Light>();
        }
        color[0] = Color.red;
        color[1] = Color.blue;
        color[2] = Color.yellow;
        color[3] = Color.green;
    }

    void Update()
    {
        // set light color
        float t = Mathf.PingPong(Time.time, duration) / duration;

        for(int i = 0; i<slight.Length; i++){
            lt[i].color = Color.Lerp(color[i], color[(i+1)%4], t);
        }
    }
}
