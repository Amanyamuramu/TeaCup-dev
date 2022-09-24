using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanNadenade : MonoBehaviour
{

// public SerialHandler serialHandler;
private Animator animator;
float m_MySliderValue;
[SerializeField] Text text;
float speed = 1f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        text.text = $"Speed = {speed.ToString("F1")}";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            speed += 0.2f;
            animator.SetFloat("Speed", speed);
            text.text = $"Speed = {speed.ToString("F1")}";
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            speed -= 0.2f;
            animator.SetFloat("Speed", speed);
            text.text = $"Speed = {speed.ToString("F1")}";
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //serial情報を送信している
            // serialHandler.Write("1");
            animator.SetBool("is_nade", true);
            transform.position = new Vector3 (0,0,0);
        }
        else
        {
            animator.SetBool("is_nade", false);
        }
    }

}
