using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputSetting : MonoBehaviour
{
    public Ros2Moving ros2Moving;
    public GameObject[] fadeobject = new GameObject[2];
    AudioSource audioSource;

    void Start()
    {
        fadeobject[0].SetActive(true);
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Stop();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            PatternA();
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            PatternB();
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            PatternC();
        }

        if(Input.GetKeyDown(KeyCode.F5)){
            TimeStart();
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            TimeStop();
        }
        if(Input.GetKeyDown(KeyCode.F7)){
            Retry();
        }
        if(Input.GetKeyDown(KeyCode.F8)){
            if(fadeobject[1].activeSelf){
                fadeobject[1].SetActive(false);
            }
            else{
                fadeobject[1].SetActive(true);
            }
        }
    }

    public void TimeStart(){
        fadeobject[0].SetActive(false);
        fadeobject[1].SetActive(false);
        Time.timeScale = 1;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();  
        Time.timeScale = 1;
        //object 表示非表示
        //audio

    }

    public void TimeStop(){
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Stop();    
        fadeobject[0].SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void PatternA(){
        if(ros2Moving.alphaBool == false){
            ros2Moving.alphaBool = true;
        }
        else{
            ros2Moving.alphaBool = false;
        }
        Debug.Log("alpha patten");
    }
    public void PatternB(){
        if(ros2Moving.betaBool == false){
            ros2Moving.betaBool = true;
        }
        else{
            ros2Moving.betaBool = false;
        }
        Debug.Log("b patten");
    }
    public void PatternC(){
        if(ros2Moving.thetaBool == false){
            ros2Moving.thetaBool = true;
        }
        else{
            ros2Moving.thetaBool = false;
        }
        Debug.Log("c patten");
    }
}
