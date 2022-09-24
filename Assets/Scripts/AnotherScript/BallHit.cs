using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
    public SerialHandler serialHandler;
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Ball_1")){
            print("hit 1");
            serialHandler.Write("a");
            serialHandler.Write("1");
        }
        else if(other.CompareTag("Ball_2")){
            print("hit 2");
            serialHandler.Write("2");
        }
        else if(other.CompareTag("Ball_3")){
            print("hit 3");
            serialHandler.Write("3");
        }
        else if(other.CompareTag("Ball_4")){
            print("hit 4");
            serialHandler.Write("4");
        }
        else if(other.CompareTag("Ball_5")){
            print("hit 5");
            serialHandler.Write("5");
        }
    }
    // Start is called before the first frame update
}
