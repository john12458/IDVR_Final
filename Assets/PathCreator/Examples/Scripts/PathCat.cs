using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathCat : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speedCat = 5;
        private float speed = 0;
        PathCreation.Examples.PathFollower pf;
        private Vector3 pos;
        float distanceTravelled;
        float maxdistance;
        public GameObject xrOrigin;
        MoveProvider mp;

        public Animator animator;



        void Start() {

            mp = xrOrigin.transform.GetComponent<MoveProvider>();
            pf = xrOrigin.transform.GetComponent<PathCreation.Examples.PathFollower>();

            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            switch(mp.currMoveState)
            {
                case MoveProvider.MoveState.RUN:
                    distanceTravelled=0;
                    maxdistance = pf.runMaxdistance +1;
                    break;
                case MoveProvider.MoveState.RIDE:
                    distanceTravelled=pf.runMaxdistance;
                    maxdistance = pf.rideMaxdistance + 2;
                    break;
                case MoveProvider.MoveState.BIKE:
                    distanceTravelled = pf.rideMaxdistance;
                    maxdistance = pf.bikeMaxdistance + 2;
                    break;
            }
            animator.SetTrigger("walk");
        }

        void Update()
        {
            speed = speedCat + pf.speed - 3;
            if (pathCreator != null)
            {
                 if(distanceTravelled < maxdistance){
                    distanceTravelled += speed * Time.deltaTime;
                }   
                pos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.position = new Vector3(pos.x, transform.position.y, pos.z);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
             
           
        }
    
        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}