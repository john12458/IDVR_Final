using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_angle : MonoBehaviour
{
    int runway = 3; //跑道寬度
    public int left_bound = -4;
    public int right_bound = 5;
    bool hasTrans; //換過跑道
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        hasTrans = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("head angle: " + (transform.eulerAngles.z%360).ToString());
        if(cam.transform.eulerAngles.z > 30.0f && cam.transform.eulerAngles.z < 60.0f && !hasTrans)
        {
                        
        }
        else if(cam.transform.eulerAngles.z%360 < 330.0f && cam.transform.eulerAngles.z%360 > 300.0f && !hasTrans)
        {
                            
        }
        //Debug.Log("position: " + transform.position.z.ToString());
        else if(cam.transform.eulerAngles.z%360 > 330 || cam.transform.eulerAngles.z%360 < 30){
            hasTrans = false;
        }
        // Debug.Log("camera angle: " + cam.transform.eulerAngles.z%360);
    }
}
