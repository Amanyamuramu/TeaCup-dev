using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlesimMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int rotationSpeed = 30;

    private Vector3 lastPos;
    private Vector3 lastRot;

    [HideInInspector] public (Vector3 linear, Vector3 angular) velocity = (new Vector3(), new Vector3());

    public TwistPublisher twistPublisher;
    public Ros2Moving ros2Moving;


    void Update()
    {
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 1){
            if(ros2Moving.betaBool){
                float T = 10.0f;
                float f =  1.0f / T;
                float sin = ros2Moving.radius * Mathf.Cos(2 * Mathf.PI * f * Time.time);
                float velocitySin = ros2Moving.changeSpeed * sin;
                this.transform.position = new Vector3(velocitySin,5.0f,0f);
                // transform.Translate(sin,0f,0f);

                velocity = CalculateVelocity(transform, lastPos, lastRot);
                lastPos = transform.position;
                lastRot = transform.rotation.eulerAngles;

                Vector3 linear = velocity.linear * 50;
                Vector3 angular = velocity.angular * 50;

                // 速度をパブリッシャーのTwistMsgインスタンスにセット
                twistPublisher.SetTwistMsgValue(linear, angular);               
            }
        }
        // 移動速度を計算
    } 

    private (Vector3 linear, Vector3 angular) CalculateVelocity(
        Transform currentTransform, Vector3 lastPos, Vector3 lastRot) {

        // 直進速度を計算
        var worldPosDiff = currentTransform.position - lastPos;
        var localPosDiff = currentTransform.InverseTransformDirection(worldPosDiff);

        // 角速度を計算
        var worldRotDiff = currentTransform.rotation.eulerAngles - lastRot;
        var localRotDiff = currentTransform.InverseTransformDirection(worldRotDiff);

        Vector3 linear = localPosDiff;
        Vector3 angular = localRotDiff * Mathf.Deg2Rad;

        (Vector3 linear, Vector3 angular) velocity = (new Vector3(), new Vector3());
        velocity.linear = linear;
        velocity.angular = angular;

        return velocity;
    }
}
