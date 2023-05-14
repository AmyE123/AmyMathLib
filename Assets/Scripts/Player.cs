namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Matrix;
    using AmyMathLib.Vector;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        #region AML_Demo
        [Header("Lerping")]
        private const float LERP_COOLDOWN = 0.5f;

        [SerializeField]
        private float _lerpSpeed = 10;

        [SerializeField]
        private float _lerpCooldown = LERP_COOLDOWN;


        private bool _hasCooledDown = true;
        private bool _hasMoved = false;
        private bool _canLerp;

        private AVector3 newPlayerPosition = AMaths.ToAVector(Vector3.zero);

        public bool HasCooledDown => _hasCooledDown;

        [Header("Mesh Manipulation")]
        [SerializeField]
        private MeshManipulation _meshManipulation;

        private float _meshScaleTimer;

        [SerializeField]
        private float _meshScaleDelay;

        [SerializeField]
        private float _meshScaleSpeed;
        #endregion //AML_Demo

        [Header("Player")]
        [SerializeField]
        private GameManager _manager;

        private MeshRenderer _meshRenderer;

        private int _playerIdIdx;
        private bool _parentSet = false;
        private bool _completedSide = false;
        private bool _isPlayerAtEnd = false;

        [SerializeField]
        protected PlayerData _playerData;

        protected bool _playerCanTakeTurn;
        private bool _playerPathBlocked = false;

        public bool HasCompletedSide => _completedSide;

        void Start()
        {
            Initialization();
        }

        void Initialization()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _playerIdIdx = (int)_playerData.PlayerIdentifier;
            _meshManipulation = GetComponent<MeshManipulation>();
            _meshScaleTimer = _meshManipulation.GetMeshTimerValue(_meshScaleSpeed);
            SetPlayerMaterial();
            SetPlayerLevelPosition();
        }

        void SetPlayerMaterial()
        {
            _meshRenderer.material = _playerData.PlayerMaterial;
        }

        void SetPlayerLevelPosition()
        {
            transform.position = _manager.CurrentLevelData.PlayersStartPos[_playerIdIdx];
        }

        void SetPlayerLevelParent()
        {
            transform.parent = _manager.CurrentLevelGameObject.transform;
            _parentSet = true;
        }

        public void ResetPlayerParams()
        {
            transform.position = Vector3.zero;
            transform.SetParent(null);

            //_meshManipulation.ScaleObject(1f, 1f, 1f);

            //_parentSet = false;
            //_completedSide = false;
            //_isPlayerAtEnd = false;

            //SetPlayerLevelPosition();       
        }

        #region AML_Demo
        void Movement()
        {
            int xMov = 1;

            if (Input.GetMouseButtonDown(1) && _playerCanTakeTurn && _hasCooledDown && !_playerPathBlocked)
            {
                _canLerp = true;
                _hasMoved = true;
                _lerpCooldown = LERP_COOLDOWN;

                // -- Old implementation --
                //transform.position = new Vector3(transform.position.x + xMov, transform.position.y, transform.position.z);

                // -- New implementation --
                // Using AVector3 for the new positions, as well as ToUnityVector to apply these.
                newPlayerPosition = new AVector3(transform.position.x + xMov, transform.position.y, transform.position.z);

                // I want to implement lerping which is why this is commented out
                //transform.position = AMaths.ToUnityVector(newPlayerPosition);
            }

            if (_canLerp)
            {
                // Implementing lerp into movement
                Lerp(gameObject, newPlayerPosition);

                if (_hasMoved)
                {
                    _lerpCooldown -= Time.deltaTime;
                }
                
                if (_lerpCooldown <= 0)
                {
                    _hasCooledDown = true;
                    _hasMoved = false;
                    
                    _lerpCooldown = 0;

                    _canLerp = false;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    _hasCooledDown = false;
                    _hasMoved = true;
                }
            }
        }

        // Lerp functionality
        void Lerp(GameObject player, AVector3 target)
        {
            var lerp = AMaths.Lerp(AMaths.ToAVector(player.transform.position), target, Time.deltaTime * _lerpSpeed);

            player.transform.position = lerp.ToUnityVector3();
        }
        #endregion //AML_Demo

        void Update()
        {
            if (_manager.CurrentLevelGameObject != null && !_parentSet)
            {
                SetPlayerLevelParent();
            }

            MovementChecks();
            Movement();
        }

        void MovementChecks()
        {
            bool islevelOnPlayerSide = (int)_manager.CurrentLevel.ActiveSide == _playerIdIdx;
            //bool isPlayerAtEnd = transform.position.x == _manager.CurrentLevelData.MaxXValues[_playerIdIdx];

            IsOnButton();
            _playerPathBlocked = IsPathBlocked();

            bool isAtMinRangeX = transform.localPosition.x > _manager.CurrentLevelData.MaxXValues[_playerIdIdx] - 0.5f;
            bool isAtMaxRangeX = transform.localPosition.x < _manager.CurrentLevelData.MaxXValues[_playerIdIdx] + 0.5f;

            if (isAtMinRangeX & isAtMaxRangeX)
            {
                _isPlayerAtEnd = true;
            }           

            if (islevelOnPlayerSide && !_isPlayerAtEnd)
            {
                _playerCanTakeTurn = true;
            }
            else
            {
                _playerCanTakeTurn = false;
            }

            if (_isPlayerAtEnd)
            {
                _completedSide = true;

                _meshScaleDelay -= Time.deltaTime;
                if (_meshScaleDelay <= 0)
                {
                    _meshScaleDelay = 0;

                    _meshManipulation.LerpScaleObject(0.1f, 0.1f, 0.1f, _meshScaleSpeed);

                    _meshScaleTimer -= Time.deltaTime;
                    if (_meshScaleTimer <= 0)
                    {
                        _meshScaleTimer = 0;

                        _canLerp = false;
                        _meshManipulation.StopScaling = true;

                        transform.gameObject.SetActive(false);
                        _meshManipulation.ScaleObject(1f, 1f, 1f);
                    }
                }

               
            }
        }

        bool IsPathBlocked()
        {
            RaycastHit hit;
            Vector3 _castDir;

            if (_playerData.PlayerIdentifier == PlayerData.PlayerSide.North)
            {
                _castDir = Vector3.right;
            }
            else
            {
                _castDir = Vector3.left;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(_castDir), out hit, Mathf.Infinity))
            {              
                Debug.DrawRay(transform.position, transform.TransformDirection(_castDir) * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(_castDir) * 1000, Color.blue);
                return false;
            }

            if (_playerData.PlayerIdentifier == PlayerData.PlayerSide.North)
            {
                if (hit.distance >= 1f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (hit.distance <= 1f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        bool IsOnButton()
        {
            RaycastHit hit;
            Vector3 _castDir;

            if (_playerData.PlayerIdentifier == PlayerData.PlayerSide.North)
            {
                _castDir = Vector3.down;
            }
            else
            {
                _castDir = Vector3.up;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(_castDir), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(_castDir) * hit.distance, Color.yellow);

                if (hit.collider.gameObject.tag == "Button")
                {
                    GameObject go = hit.collider.gameObject;
                    if (go != null)
                    {
                        GameObject wall = go.GetComponentInChildren<Transform>().gameObject;
                        Destroy(wall, 2f);

                        Debug.Log("On Button");
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(_castDir) * 1000, Color.blue);
                return false;
            }

        }
    }
}