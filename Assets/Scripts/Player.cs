namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Vector;
    using UnityEngine;

    /// <summary>
    /// A class which represents each player in the game
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region AML_Demo
        [Header("Lerping")]
        private const float LERP_COOLDOWN = 0.5f;

        [SerializeField]
        [Tooltip("The speed which the player linearly interpolates on movement")]
        private float _lerpSpeed = 10;

        [SerializeField]
        [Tooltip("How fast the player recovers from a lerp movement")]
        private float _lerpCooldown = LERP_COOLDOWN;

        private bool _hasCooledDown = true;
        private bool _hasMoved = false;
        private bool _canLerp;

        private AVector3 newPlayerPosition = AMaths.ToAVector(Vector3.zero);

        /// <summary>
        /// Whether the player has cooled down from movement
        /// </summary>
        public bool HasCooledDown => _hasCooledDown;

        [Header("Mesh Manipulation")]
        [SerializeField]
        [Tooltip("The mesh manipulation component for players to reference when pressing interactive buttons")]
        private MeshManipulation _meshManipulation;

        [SerializeField]
        [Tooltip("The delay when scaling the mesh on level completion")]
        private float _meshScaleDelay;

        [SerializeField]
        [Tooltip("The speed which the player scales on level completion")]
        private float _meshScaleSpeed;

        private float _meshScaleTimer;

        private MeshRenderer _meshRenderer;
        #endregion //AML_Demo

        [Header("Player")]
        [SerializeField]
        [Tooltip("The universal game manager")]
        private GameManager _manager;

        private int _playerIdIdx;
        private bool _parentSet = false;
        private bool _completedSide = false;
        private bool _isPlayerAtEnd = false;
        private bool _playerPathBlocked = false;

        [SerializeField]
        [Tooltip("The player's data")]
        protected PlayerData _playerData;
        protected bool _playerCanTakeTurn;

        /// <summary>
        /// Whether the player has completed their side of the level
        /// </summary>
        public bool HasCompletedSide => _completedSide;

        /// <summary>
        /// Whether the player can take their turn
        /// </summary>
        public bool PlayerCanTakeTurn => _playerCanTakeTurn;

        private void Start()
        {
            Initialization();
        }

        private void Update()
        {
            if (_manager.CurrentLevelGameObject != null && !_parentSet)
            {
                SetPlayerLevelParent();
            }

            MovementChecks();
            Movement();
        }

        private void Initialization()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _playerIdIdx = (int)_playerData.PlayerIdentifier;
            _meshManipulation = GetComponent<MeshManipulation>();
            _meshScaleTimer = _meshManipulation.GetMeshTimerValue(_meshScaleSpeed);
            SetPlayerMaterial();
            SetPlayerLevelPosition();
        }

        private void SetPlayerMaterial()
        {
            _meshRenderer.material = _playerData.PlayerMaterial;
        }

        private void SetPlayerLevelPosition()
        {
            transform.position = _manager.CurrentLevelData.PlayersStartPos[_playerIdIdx];
        }

        private void SetPlayerLevelParent()
        {
            transform.parent = _manager.CurrentLevelGameObject.transform;
            _parentSet = true;
        }

        private bool IsPathBlocked()
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

        private bool IsOnButton()
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
                        //Transform btn = go.GetComponentInChildren<Transform>();
                        //GameObject wall = btn.GetChild(0).gameObject;

                        // Had an issue with shrinking it which I didn't have time to fix
                        //wall.GetComponent<BlockingCube>().ShrinkWall();

                        Destroy(go, 0.5f);

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

        #region AML_Demo
        private void Movement()
        {
            // Use this to add player charge to movement
            //TODO: Fix the player charge implementation
            int xMov = _manager.PlayerCharge;

            if (Input.GetMouseButtonDown(0) && _playerCanTakeTurn && _hasCooledDown && !_playerPathBlocked)
            {
                _canLerp = true;
                _hasMoved = true;
                _lerpCooldown = LERP_COOLDOWN;

                // -- Old implementation --
                //transform.position = new Vector3(transform.position.x + xMov, transform.position.y, transform.position.z);

                // -- New implementation --
                // Using AVector3 for the new positions, as well as ToUnityVector to apply these.
                newPlayerPosition = new AVector3(transform.position.x + 1, transform.position.y, transform.position.z);

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
        private void Lerp(GameObject player, AVector3 target)
        {
            var lerp = AMaths.Lerp(AMaths.ToAVector(player.transform.position), target, Time.deltaTime * _lerpSpeed);

            player.transform.position = lerp.ToUnityVector3();
        }

        private void MovementChecks()
        {
            bool islevelOnPlayerSide = (int)_manager.CurrentLevel.ActiveSide == _playerIdIdx;

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
        #endregion //AML_Demo
    }
}