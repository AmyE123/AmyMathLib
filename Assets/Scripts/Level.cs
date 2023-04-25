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

        private bool _rotating;

        private AQuaternion _finalrot;

        private AQuaternion _startRot;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();

            _finalrot = new AQuaternion(180 * Mathf.Deg2Rad, new AVector3(0, 0, 1)) * AQuaternion.ToAQuaternion(transform.rotation);
            _startRot = AQuaternion.ToAQuaternion(transform.rotation);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                var rot = AQuaternion.ToUnityQuaternion(AQuaternion.Slerp(_startRot, _finalrot, 0.5f));
                transform.rotation = rot;
                Debug.Log($"sROT: {_startRot.w}, {_startRot.v.x}, {_startRot.v.y}, {_startRot.v.z}");
                Debug.Log($"fROT: {_finalrot.w}, {_finalrot.v.x}, {_finalrot.v.y}, {_finalrot.v.z}");
                Debug.Log($"ROT: {rot}");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                print(transform.rotation);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _manager.PlayersCooledDown())
            {
                if (ActiveSide == Sides.North)
                {
                    Debug.Log("[LEVEL] Turning North -> South");
                    ActiveSide = Sides.South;

                    //transform.Rotate(180, 0, 0);

                    var ROT = new AQuaternion(180, new AVector3(0, 0, 180));
                    //transform.rotation = AQuaternion.ToUnityQuaternion(ROT);
                    _slerpTime = 0;
                    _rotating = true;
                    
                }
                else
                {
                    Debug.Log("[LEVEL] Turning South -> North");
                    ActiveSide = Sides.North;

                    //transform.Rotate(-180, 0, 0);
                    _rotating = false;
                    var ROT = new AQuaternion(180, new AVector3(0, 0, 0));
                    //transform.rotation = AQuaternion.ToUnityQuaternion(ROT);
                    //SlerpRotate(AQuaternion.ToAQuaternion(transform.rotation), new AQuaternion(180, new AVector3(0, 0, 1)));
                }
            }

            if (_rotating == true)
            {                
                SlerpRotate(_startRot, _finalrot);

                //transform.rotation = AQuaternion.ToUnityQuaternion(final);
            }
        }

        void SlerpRotate(AQuaternion currentRot, AQuaternion targetRot)
        {
            _slerpTime += Time.deltaTime * _slerpSpeed;
            _slerpTime = Mathf.Clamp(_slerpTime, 0, 1);

            // Defining two rotations
            //AQuaternion currentRot = AQuaternion.ToAQuaternion(transform.rotation);
            //AQuaternion targetRot = new AQuaternion(180, new AVector3(0, 0, xRot));

            // This is the slerped value         
            AQuaternion slerped = AQuaternion.Slerp(currentRot, targetRot, _slerpTime);

            Debug.Log($"AE: slerped z {slerped.v.z} / / _slerptime {_slerpTime}");

            //// Define a vector which we will rotate
            //AVector3 p = new AVector3(1, 2, 3);

            //// Store that vector in a quaternion (We're using an overloaded constructor here to store the raw position)
            //AQuaternion k = new AQuaternion(p);

            //// The variable newK will have our new position inside of it
            //AQuaternion newK = slerped * k * slerped.Inverse();

            //// Get the position as a vector
            //AVector3 newP = newK.GetAxis();

            // Set the position so we can see if it's working
            transform.rotation = AQuaternion.ToUnityQuaternion(slerped);
        }
    }
}