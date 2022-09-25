using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;


public class TwistPublisher : MonoBehaviour
{
    public string topicName = "yamaha/cmd_vel";
    public float publishMessageFrequency = 0.5f;
    private ROSConnection ros;
    private float timeElapsed;
    private TwistMsg twist = new TwistMsg();
    public Ros2Moving ros2Moving;


    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<TwistMsg>(topicName);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        // 一定周期でパブリッシュする
        if (timeElapsed > publishMessageFrequency)
        {
            SendTwistMsg(twist);
            timeElapsed = 0;
        }
        
    }
    public void SendTwistMsg(TwistMsg msg)
    {
        ros.Publish(topicName, msg);
    }

    public void SendTwistMsgInstance()
    {
        ros.Publish(topicName, twist);
    }

    public void SetTwistMsgValue(Vector3 linearVector, Vector3 angularVector) {
        // 移動速度と回転速度をそれぞれROSの座標系に変換
        Vector3<FLU> rosLinear = linearVector.To<FLU>();
        Vector3<FLU> rosAngular = angularVector.To<FLU>();
        
        if(ros2Moving.betaBool){
            rosAngular.x = 0.0f;
            rosAngular.y = 0.0f;
            rosAngular.z = 0.0f;

            rosLinear.x = rosLinear.y;
            rosLinear.y = 0f;
            rosLinear.z = 0f;
        }
        if(ros2Moving.alphaBool){
            rosAngular.x = 0.0f;
            rosAngular.y = 0.0f;

            rosLinear.x = 0f;
            rosLinear.y = 0f;
            rosLinear.z = 0f;
        }
        if(ros2Moving.alphaBool){
            rosAngular.x = 0.0f;
            rosAngular.y = 0.0f;

            rosLinear.x = rosLinear.y;
            rosLinear.y = 0f;
            rosLinear.z = 0f;
        }


        
        

        // 速度をTwistMsgインスタンスにセット
        twist.linear = rosLinear;
        twist.angular = rosAngular;
    }
    
}
