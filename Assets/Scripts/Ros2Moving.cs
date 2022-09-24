using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ros2Moving : MonoBehaviour
{

    [HideInInspector] private Vector3 linear = new Vector3(0f, 0f, 0f);
    [HideInInspector] private Vector3 angular = new Vector3(0f, 0f, 0f);

    public TwistPublisher twistPublisher;
    public float radius = 0.5f;

    [Header("周期(s):一周するのにかかる時間")]
    public float timeSpan = 10.0f;

    public bool alphaBool,betaBool,thetaBool;

    [Header("easingのための閾値")]
    public float thresold = 0.8f;
    public float thresold_receive = 0.01f;
    public float changeSpeed = 3.6f;


    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(Time.timeScale == 1){
            if(alphaBool){
                radius = 0f;
                linear.z = changeSpeed * 2*Mathf.PI*radius / timeSpan;
                angular.y = changeSpeed * 2*Mathf.PI / timeSpan;
                twistPublisher.SetTwistMsgValue(linear, angular);
            }
            //sinWaveの処理
            else if(betaBool){

            }
            else if(thetaBool){
                radius = 0.5f;
                linear.z = changeSpeed * 2*Mathf.PI*radius / timeSpan;
                angular.y = changeSpeed * 2*Mathf.PI / timeSpan;
                twistPublisher.SetTwistMsgValue(linear, angular);
                
            }
            //easingの処理
            else{
                if(Mathf.Abs(angular.y) > thresold_receive || Mathf.Abs(linear.x) > thresold_receive){
                    angular.y = angular.y * thresold;
                    linear.x = linear.x * thresold;
                }
                else{
                    linear = Vector3.zero;
                    angular = Vector3.zero;
                }
                twistPublisher.SetTwistMsgValue(linear, angular);
            }
        }
    }
    // private void CalculateCircleMove{

    // }
    //easingするような処理 
}
