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

        [SerializeField]
        private bool _hasCooledDown = true;

        [SerializeField]
        private bool _hasMoved = false;

        private bool _canLerp;

        private AVector3 newPlayerPosition = AMaths.ToAVector(Vector3.zero);

        public bool HasCooledDown => _hasCooledDown;
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

        public bool HasCompletedSide => _completedSide;

        void Start()
        {
            Initialization();
        }

        void Initialization()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _playerIdIdx = (int)_playerData.PlayerIdentifier;
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

        #region AML_Demo
        void Movement()
        {
            int xMov = 1;

            if (Input.GetMouseButtonDown(1) && _playerCanTakeTurn && _hasCooledDown)
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
            }
        }
    }
}