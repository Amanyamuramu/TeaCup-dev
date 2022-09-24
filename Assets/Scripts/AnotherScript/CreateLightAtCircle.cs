using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightAtCircle : MonoBehaviour
{
    [SerializeField] public int itemCount = 12;
    [SerializeField] public GameObject createObject;
    [SerializeField] public float radius = 1.5f;
    [SerializeField] public float height = 1.5f;

    public float timeSpan = 1.0f;
    //親のゲームオブジェクト
    public GameObject parent;

    private GameObject[] NewObject;

    private float time = 0f;

    public float angleStep = 50f;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        NewObject = new GameObject[itemCount];

        creates(height,radius);
        //すべてcloneになるので、生成時に名前を変更するか、gameobject方の配列に代入するのが望ましい
    }

    public void creates(float height, float radius){

        for(var i = 0; i < itemCount; ++i){

            var point = ((float)i / itemCount) * 2.0 * Mathf.PI;
            var repeatPoint = point * 1f;

            var x = Mathf.Cos((float)repeatPoint) * radius;
            var y = Mathf.Sin((float)repeatPoint) * radius;

            var position = new Vector3(x,0f+height,y);

            var obj = Instantiate(
                createObject,
                position,
                // Quaternion.Euler(0f,-360/12f*i,0f),
                Quaternion.Euler(0f,-360/itemCount*i,0f),
                transform
            ) as GameObject;

            obj.name = "Light"+i;
            
            NewObject[i] = parent.transform.Find("Light"+i).gameObject;
            NewObject[i].SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < itemCount; i++){
             if(time<=i && i<time+1){
                 NewObject[i].SetActive(true);
                //  NewObject[i].transform.Rotate(100 * Time.deltaTime,0,0);
                NewObject[i].transform.Rotate(0,0,-angleStep * Time.deltaTime);
             }
             else{
                NewObject[i].transform.Rotate(0,0,angleStep * Time.deltaTime / (itemCount+1));
                NewObject[i].SetActive(false);
             }
         }
         time += Time.deltaTime*timeSpan;
         Debug.Log ((time)+"(秒)");
 
         if(time > itemCount){
             time = 0f;
         }
        
    }
}
