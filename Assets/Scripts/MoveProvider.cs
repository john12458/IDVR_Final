using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveProvider : LocomotionProvider
{
    
    public XRBaseController xr;
    public XRBaseController xr2;
    public XRBaseController tracker;
    public CharacterController playerOrigin;
    public GameObject cam;

    public enum MoveState { RUN,RIDE,BIKE}
    public MoveState currMoveState;
    float rx, py, py2, negativeAcc;

    bool canMove;
    PathCreation.Examples.PathFollower pf;
    // Start is called before the first frame update

    // Bike attribute
    public GameObject bikeObject;
    public float bikeSpeed = 10;
    float bikeVSpeed = 0;
    float currTime = 0;

    void Start()
    {
        
        canMove = false;
        pf = transform.GetComponent<PathCreation.Examples.PathFollower>();
        pf.speed = 0;
        rx = xr.transform.eulerAngles.x;
        py = xr.transform.position.y;
        py2 = xr2.transform.position.y;
        Debug.Log("ride");
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void run(){
        float foward_value = Mathf.Abs(xr.transform.eulerAngles.x - rx);
        // float foward_value = Mathf.Abs(xr.transform.eulerAngles.x-90);
        // foward_value *= 0.05f;
        // Debug.Log(xr.transform.eulerAngles.x + "," +xr.transform.eulerAngles.y+ ","+xr.transform.eulerAngles.z ) ;
        // transform.position += foward_value *new Vector3( cam.transform.forward.x, 0 ,  cam.transform.forward.z);
        if(foward_value < 50){
            pf.speed = foward_value*2;
        }
        else{
            pf.speed = 35;
        }

        
        rx = xr.transform.eulerAngles.x;
        // Debug.Log("xr = "+ pf.speed ) ;

    }

    void ride(){
        negativeAcc = 0.2f;
        if(pf.speed-negativeAcc < 0) pf.speed = 0;
        else pf.speed -= negativeAcc;

        float foward_value = Mathf.Abs(xr.transform.position.y-py);
        float foward_value2 = Mathf.Abs(xr2.transform.position.y-py2);
        Debug.Log("ride test " + foward_value);
        Debug.Log("ride test foward_value2  " + foward_value2);
        if(currTime >= 0.1f){
            if(foward_value > 0.1f && foward_value2 > 0.1f){

                pf.speed += 3;
                pf.speed = Mathf.Min(35, pf.speed);
                Debug.Log("speed: " + pf.speed);
            }

            py = xr.transform.position.y;
            py2 = xr2.transform.position.y;
            currTime = 0;
        }

        if (pf.speed > 20) { 
             locomotionPhase = LocomotionPhase.Moving;
        }else {
            locomotionPhase = LocomotionPhase.Done;
        }

    }
    void LatedUpdate() { 

        //  if(canMove){
        //     switch(currMoveState)
        //     {
        //         case MoveState.RUN:
        //             break;
        //         case MoveState.RIDE:                    
        //             if (pf.speed > 20) { 
        //                 locomotionPhase = LocomotionPhase.Moving;
        //             }else {
        //                 locomotionPhase = LocomotionPhase.Idle;
        //             }
        //             break;
        //         case MoveState.BIKE:
        //             break;
        //     }
        // }

    }

    void bike(){
        Transform camtransform = cam.GetComponent<Transform>();
        // init
        if(pf.enabled == true){
            bikeObject.SetActive(true);
            UnityEngine.InputSystem.XR.TrackedPoseDriver track_pose_driver = cam.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
            track_pose_driver.trackingType = UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType.RotationOnly;
            // float cam_height = 1.2f;
            // // 防止camera卡在車裡面
            // camtransform.localPosition  = new Vector3(camtransform.localPosition.x, cam_height, camtransform.localPosition.z);
            pf.enabled = false;
        }

        // exceed
        if(playerOrigin.isGrounded)
            bikeVSpeed = 0;
        
        Vector3 dir = tracker.transform.forward;
        bikeVSpeed -= 9.8f * Time.deltaTime;
        dir.y = bikeVSpeed;
        playerOrigin.Move(dir * bikeSpeed * Time.deltaTime);
        
        // bikeObject rotation with tracker
        float rotationValue= tracker.transform.eulerAngles.y;
         bikeObject.transform.eulerAngles = new Vector3(
            bikeObject.transform.eulerAngles.x,
            rotationValue,
            bikeObject.transform.eulerAngles.z);

    }
    void Update()
    {
        currTime += Time.deltaTime;
        if(canMove){
            switch(currMoveState)
            {
                case MoveState.RUN:
                    run();
                    break;
                case MoveState.RIDE:                    
                    ride();
                    break;
                case MoveState.BIKE:
                    bike();
                    break;
            }
        }
        // act according to the state
          
        
    }

    private IEnumerator wait(){
        Debug.Log("www");
        yield return new WaitForSeconds(1);
        Debug.Log("wait");
        canMove = true;
    }

}
