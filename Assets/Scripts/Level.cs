namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Quaternion;
    using AmyMathLib.Vector;
    using UnityEngine;

    public class Level : MonoBehaviour
    {
        [Header("Level Data")]
        [SerializeField]
        private GameManager _manager;

        public enum Sides { North, South };

        public Sides ActiveSide = Sides.North;

        [Header("Visual Data")]
        [SerializeField]
        private float _slerpTime;

        [SerializeField]
        private float _slerpSpeed;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _manager.PlayersCooledDown())
            {
                if (ActiveSide == Sides.North)
                {
                    Debug.Log("[LEVEL] Turning North -> South");
                    ActiveSide = Sides.South;

                    //transform.Rotate(180, 0, 0);

                    var ROT = new AQuaternion(180, new AVector3(0, 0, 180));
                    transform.rotation = AQuaternion.ToUnityQuaternion(ROT);
                    //SlerpRotate(180);
                }
                else
                {
                    Debug.Log("[LEVEL] Turning South -> North");
                    ActiveSide = Sides.North;

                    //transform.Rotate(-180, 0, 0);

                    var ROT = new AQuaternion(180, new AVector3(0, 0, 0));
                    transform.rotation = AQuaternion.ToUnityQuaternion(ROT);
                    //SlerpRotate(0);
                }
            }
        }

        void SlerpRotate(float xRot)
        {
            _slerpTime += Time.deltaTime * _slerpSpeed;

            // Defining two rotations
            AQuaternion currentRot = AQuaternion.ToAQuaternion(transform.rotation);
            AQuaternion targetRot = new AQuaternion(180, new AVector3(0, 0, xRot));

            // This is the slerped value
            AQuaternion slerped = AQuaternion.Slerp(currentRot, targetRot, _slerpTime);

            // Define a vector which we will rotate
            AVector3 p = new AVector3(1, 2, 3);

            // Store that vector in a quaternion (We're using an overloaded constructor here to store the raw position)
            AQuaternion k = new AQuaternion(p);

            // The variable newK will have our new position inside of it
            AQuaternion newK = slerped * k * slerped.Inverse();

            // Get the position as a vector
            AVector3 newP = newK.GetAxis();

            // Set the position so we can see if it's working
            transform.rotation = AQuaternion.ToUnityQuaternion(newP);
        }
    }
}