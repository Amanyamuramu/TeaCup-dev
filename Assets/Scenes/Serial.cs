using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

 public class Serial : MonoBehaviour
 {
     //通信パラメータ
     private SerialPort serialPort_ = new SerialPort("COM3", 38400, Parity.Even, 8, StopBits.One);


     //電動車椅子の速度・角速度
     public float linear = 0.2F; //速度[km/h](-5.6~5.6)
    //  float angular =0.0F;
     static float angular = 0.1F; //角速度[km/h]


     //電動車椅子の情報
     float Perimeter = 1.18F; //タイヤ周長[m]
     float Gear_Ratio = 12.64F; //ギア比[-]
     float Distance = 0.24F; //電動車椅子の中心から車輪までの距離[m]
     float Turning_Radius =0.0F; //電動車椅子の旋回半径[m]

     //右のタイヤの回転数・速度
     float rpm_right; //電動車椅子の右のタイヤの回転数[rpm]
     float velocity_right; //電動車椅子の右のタイヤの速度[km/h]
     float Velocity_right; //電動車椅子の右のタイヤの速度[m/s]

     //左のタイヤの回転数・速度
     float rpm_left; //電動車椅子の左のタイヤの回転数[rpm]
     float velocity_left; //電動車椅子の左のタイヤの速度[km/h]
     float Velocity_left; //電動車椅子の左のタイヤの速度[m/s]

     //電動車椅子の中心の速度・角速度
     float Angular = angular/3.6F; //角速度[m/s]




     void Start()
     {
        

         serialPort_.Open();
         
         //RequestCommand
         byte[] RequestCommand = new byte[15]; //配列の作成
         //BaseFrame
         RequestCommand[0] = 0x01; //ヘッダ
         RequestCommand[1] = 0x08; //ソースアドレス
         RequestCommand[2] = 0x03; //ディスティネーションアドレス
         RequestCommand[3] = 0x01; //通信番号
         RequestCommand[4] = 0x68; //コマンド
         RequestCommand[5] = 0x08; //データ長
         //Data
         RequestCommand[6] = 0x01; //モード（アカデミックモード＝01h）
         RequestCommand[7] = 0x00; //
         RequestCommand[8] = 0x00; //
         RequestCommand[9] = 0x00; //
         RequestCommand[10] = 0x00; //タイムアウト(無効=00h)
         RequestCommand[11] = 0x00; //
         RequestCommand[12] = 0x00; //
         RequestCommand[13] = 0x00; //
         //CheckSum
         int sum = 0;
         for (int i = 1; i < 13; i++)
         {
             sum += RequestCommand[i];
         }
         RequestCommand[14] = (byte)sum; //CheckSum（これまでの数値の和の下位１バイト）

         //Request Command 送信
         int offset_Write = 0;
         int count_Write = 15;
         serialPort_.Write(RequestCommand, offset_Write, count_Write);

         //Request Command 受信
         byte[] inBuffer = new byte[15];
         int offset_Read = 0;
         int count_Read = 15;
         int read;

         while (count_Read > 0 && (read = serialPort_.Read(inBuffer, offset_Read, count_Read)) > 0) //データが送られている間はinBufferにデータを入れる
         {
             offset_Read += read;
             count_Read -= read;

             for (int i = 1; i < 15; i++)
             {
                //  Debug.Log(inBuffer[i]); //受信データの表示
             }
         }
     }




     void Update()
     {

         try
         {   
            //angular が 0の時に0で割っているので、これがエラーの原因。
             Turning_Radius = linear / angular;
             //right tire
             velocity_right = (Turning_Radius + Distance) * Angular; //m/s
             Velocity_right = velocity_right * 3.6F; //km/h
             rpm_right = (1000 * Velocity_right * Gear_Ratio) / (60 * Perimeter);
             //left tire
             velocity_left = (Turning_Radius - Distance) * Angular; //m/s
             Velocity_left = velocity_left * 3.6F; //km/h
             rpm_left = (1000 * Velocity_left * Gear_Ratio) / (60 * Perimeter);
             print(rpm_right);
         }
         catch (System.DivideByZeroException)
         {
             Turning_Radius = 0.0F;
             rpm_right = (1000 * linear * Gear_Ratio) / (60 * Perimeter);
             rpm_left = (1000 * linear * Gear_Ratio) / (60 * Perimeter);


         }

         //WriteReadCommand
         byte[] WriteReadCommand = new byte[23];
         //BaseFrame
         WriteReadCommand[0] = 0x01; //ヘッダ
         WriteReadCommand[1] = 0x08; //ソースアドレス
         WriteReadCommand[2] = 0x03; //ディスティネーションアドレス
         WriteReadCommand[3] = 0x01; //通信番号
         WriteReadCommand[4] = 0x69; //コマンド
         WriteReadCommand[5] = 0x10; //データ長
         //Data
         WriteReadCommand[6] = 0x02; //ユニット有効指示(モータ回転指示値=02h)
         WriteReadCommand[7] = 0x00; //
         WriteReadCommand[8] = 0x00; //
         WriteReadCommand[9] = 0x00; //
         WriteReadCommand[10] = 0x00; //
         WriteReadCommand[11] = 0x00; //
         WriteReadCommand[12] = (byte)((int)rpm_right & 0x00FF); //右モーター回転指示（下位バイト） (-1000rpm~1000rpm)
         WriteReadCommand[13] = (byte)(((int)rpm_right & 0xFF00) >> 8); ////右モーター回転指示（上位バイト） (-1000rpm~1000rpm)
         WriteReadCommand[14] = (byte)((int)rpm_left & 0x00FF); ////左モーター回転指示（下位バイト） (-1000rpm~1000rpm)
         WriteReadCommand[15] = (byte)(((int)rpm_left & 0xFF00) >> 8); ////左モーター回転指示（上位バイト） (-1000rpm~1000rpm)
         WriteReadCommand[16] = 0x00; //
         WriteReadCommand[17] = 0x00; //
         WriteReadCommand[18] = 0x00; //
         WriteReadCommand[19] = 0x00; //
         WriteReadCommand[20] = 0x00; //
         WriteReadCommand[21] = 0x00; //
         //CheckSum
         int sum = 0;
         for (int i = 1; i < 21; i++)
         {
             sum += WriteReadCommand[i];
         }
         WriteReadCommand[22] = (byte)sum; //CheckSum（これまでの数値の和の下位１バイト）

         //WriteReadCommand 送信
         int offset_Write = 0;
         int count_Write = 23;
         serialPort_.Write(WriteReadCommand, offset_Write, count_Write);

         //WriteReadCommand 受信
         byte[] inBuffer = new byte[23];
         int offset_Read = 0;
         int count_Read = 23;
         int read;

         while (count_Read > 0 && (read = serialPort_.Read(inBuffer, offset_Read, count_Read)) > 0)
         {
             offset_Read += read;
             count_Read -= read;

             for (int i = 1; i < 23; i++)
             {
                //  print(inBuffer[i]);
             }

             //change rpm to velocity

             //right
             int right_lower = inBuffer[8];
             int right_upper = inBuffer[9];
             float rpm_Right = (right_upper << 8) + right_lower;
            //  float speed_right = rpm_right * 60 / 1000 * tire / gear;
             //left
             int left_lower = inBuffer[10];
             int left_upper = inBuffer[11];
             float rpm_left = (left_upper << 8) + left_lower;
            //  float speed_left = rpm_left * 60 / 1000 * tire / gear;

         }

     }
 }