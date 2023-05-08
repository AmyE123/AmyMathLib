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

        private bool _rotatingToSouth;
        private bool _rotatingToNorth;

        private AQuaternion _startRotNorth;
        private AQuaternion _startRotSouth;

        private AQuaternion _finalRot;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();

            _finalRot = new AQuaternion(180 * Mathf.Deg2Rad, new AVector3(0, 0, 1)) * AQuaternion.ToAQuaternion(transform.rotation);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _manager.PlayersCooledDown())
            {
                if (ActiveSide == Sides.North)
                {
                    _startRotNorth = AQuaternion.ToAQuaternion(transform.rotation);
                    _finalRot = new AQuaternion(180 * Mathf.Deg2Rad, new AVector3(0, 0, 1)) * AQuaternion.ToAQuaternion(transform.rotation);

                    Debug.Log("[LEVEL] Turning North -> South");
                    ActiveSide = Sides.South;

                    // -- Old implementation --
                    //transform.Rotate(180, 0, 0);

                    // -- New implementation --
                    _slerpTime = 0;
                    _rotatingToSouth = true;
                    _rotatingToNorth = false;
                }
                else
                {
                    _startRotSouth = AQuaternion.ToAQuaternion(transform.rotation);
                    _finalRot = new AQuaternion(-180 * Mathf.Deg2Rad, new AVector3(0, 0, 1)) * AQuaternion.ToAQuaternion(transform.rotation);

                    Debug.Log("[LEVEL] Turning South -> North");
                    ActiveSide = Sides.North;

                    // -- Old implementation --
                    //transform.Rotate(-180, 0, 0);

                    // -- New implementation --
                    _slerpTime = 0;
                    _rotatingToSouth = false;
                    _rotatingToNorth = true;
                }
            }

            if (_rotatingToSouth)
            {
                SlerpRotate(_startRotNorth, _finalRot);
            }
            if (_rotatingToNorth)
            {
                SlerpRotate(_startRotSouth, _finalRot);
            }
        }

        // -- New implementation --
        void SlerpRotate(AQuaternion currentRot, AQuaternion targetRot)
        {
            _slerpTime += Time.deltaTime * _slerpSpeed;
            _slerpTime = Mathf.Clamp(_slerpTime, 0, 1);

            // This is the slerped value         
            AQuaternion slerped = AQuaternion.Slerp(currentRot, targetRot, _slerpTime);

            // Set the position so we can see if it's working
            transform.rotation = AQuaternion.ToUnityQuaternion(slerped);
        }
    }
}