using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputSetting : MonoBehaviour
{
    public Ros2Moving ros2Moving;
    public GameObject fadeobject;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeobject.SetActive(true);
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Stop();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F1)){
            if(ros2Moving.alphaBool == false){
            ros2Moving.alphaBool = true;
            }
        else{
            ros2Moving.alphaBool = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            if(ros2Moving.betaBool == false){
            ros2Moving.betaBool = true;
        }
        else{
            ros2Moving.betaBool = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            if(ros2Moving.thetaBool == false){
            ros2Moving.thetaBool = true;
            }
        else{
            ros2Moving.thetaBool = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.F5)){
            fadeobject.SetActive(false);
            Time.timeScale = 1;
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Play();  
            Time.timeScale = 1;
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Stop();    
            fadeobject.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.F7)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TimeStart(){
        fadeobject.SetActive(false);
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
        fadeobject.SetActive(true);
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
