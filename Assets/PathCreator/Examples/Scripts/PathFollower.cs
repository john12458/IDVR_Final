using UnityEngine;
using DG.Tweening;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 0;
        float distanceTravelled ;
        float maxdistance;
        public float runMaxdistance;
        public float rideMaxdistance;
        public float bikeMaxdistance;

        PathCreator path_L;
        PathCreator path_R;
        private bool isLeft = true;
        public GameObject cam = null;

        private Vector3 pos;
        private Vector3 posL;
        private Vector3 posR;
        MoveProvider mp;
        float timer;
        float pz, pz2;
        private GameObject bear;

        public float ratio = 0;
        void Start() {
            timer = 0;
            mp = transform.GetComponent<MoveProvider>();
            bear = GameObject.FindWithTag("Bear");
            runMaxdistance = 300;
            rideMaxdistance = 800;
            bikeMaxdistance = 2000;
            pz = mp.xr.transform.position.z;
            pz2 = mp.xr2.transform.position.z;
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

            // path_L = GameObject.Find("path_L").GetComponent<PathCreator>();
            // path_R = GameObject.Find("path_R").GetComponent<PathCreator>();

            switch(mp.currMoveState)
            {
                case MoveProvider.MoveState.RUN:
                    ratio = 0;
                    bear.SetActive(false);
                    distanceTravelled=0;
                    maxdistance = runMaxdistance;
                    break;
                case MoveProvider.MoveState.RIDE:
                    Debug.Log("ride mode");
                    ratio = 1.6f;
                    bear.SetActive(true);
                    distanceTravelled = runMaxdistance;
                    maxdistance = rideMaxdistance;

                    break;
                case MoveProvider.MoveState.BIKE:
                    bear.SetActive(false);
                    distanceTravelled = rideMaxdistance;
                    // transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    maxdistance = bikeMaxdistance;
                    break;
            }  
           
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (pathCreator != null )
            {
                if(distanceTravelled < maxdistance){
                    distanceTravelled += speed * Time.deltaTime;
                }
                if (distanceTravelled >= rideMaxdistance){
                    ratio = 0;
                    bear.SetActive(false);                  
                }

                pos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) + transform.up * ratio;
                
                
                
                // transform.position = new Vector3(pos.x, transform.position.y, pos.z);
                // transform.position = pos; 
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

                posL = pos - transform.right*1.5f;
                posR = pos + transform.right*0.5f;

                if(isLeft){
                    transform.position = posL;
                }
                else{
                    
                    transform.position = posR;
                }

  
                // Debug.Log("distanceTravelled = "+distanceTravelled );
            }
            switch(mp.currMoveState)
            {
                case MoveProvider.MoveState.RUN:
                    if(cam.transform.eulerAngles.z > 15.0f && cam.transform.eulerAngles.z < 30.0f)
                    {
                        
                        transform.DOMove(posL, 0.3f);   
                        // transform.position = posL;
                        isLeft = true;
                    }
                    else if(cam.transform.eulerAngles.z%360 < 345.0f && cam.transform.eulerAngles.z%360 > 330.0f)
                    {
                        
                        transform.DOMove(posR, 0.3f);    
                        // transform.position = posR;  
                        isLeft = false;
                    }
                    break;
                case MoveProvider.MoveState.RIDE:
                    // if(timer >= 0.06f){
                    //     // Debug.Log("turn: "+ (pz - mp.xr.transform.position.z));
                    //     if(mp.xr.transform.position.z -pz > 0.1f && pz - mp.xr.transform.position.z < 10)
                    //     {

                    //         transform.position = posL;
                    //         isLeft = true;
                    //     }
                    //     else if(mp.xr2.transform.position.z - pz2> 0.1f && pz2 - mp.xr2.transform.position.z < 10)
                    //     {

                    //         transform.position = posR;  
                    //         isLeft = false; 
                    //     }
                    //     pz =  mp.xr.transform.position.z;
                    //     pz2 =  mp.xr2.transform.position.z;
                    //     timer=0;
                    // }
                    pz = mp.xr.transform.localPosition.z;
                    pz2 = mp.xr2.transform.localPosition.z;

                    if(pz - pz2 > 0.1f){
                        Debug.Log("turn left" + pz);
                        transform.DOMove(posL, 0.3f);
                        isLeft = true;
                    }
                    else if(pz2 - pz > 0.1f){
                        Debug.Log("turn right" + pz2);
                        transform.DOMove(posR, 0.3f);
                        isLeft = false;
                    }


                    break;
                case MoveProvider.MoveState.BIKE:
                    
                    break;
            } 

            

            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        
        
    }
    
}