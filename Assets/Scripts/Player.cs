namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Vector;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameManager _manager;

        [SerializeField]
        private PlayerData _playerData;

        private MeshRenderer _meshRenderer;

        private int _playerIdIdx;

        private bool _parentSet = false;

        private bool _canMove;

        private bool _completedSide = false;

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

        void PlayerMovement()
        {
            int xMov = _playerData.XMovement;

            if (Input.GetMouseButtonDown(1) && _canMove)
            {
                // -- Old implementation --
                //transform.position = new Vector3(transform.position.x + xMov, transform.position.y, transform.position.z);

                // -- New implementation --
                // Using AVector3 for the new positions, as well as ToUnityVector to apply these.
                AVector3 newPlayerPosition = new AVector3(transform.position.x + xMov, transform.position.y, transform.position.z);
                transform.position = AMaths.ToUnityVector(newPlayerPosition);

            }
        }

        void Update()
        {
            if (_manager.CurrentLevelGameObject != null && !_parentSet)
            {
                SetPlayerLevelParent();
            }

            MovementChecks();
            PlayerMovement();
        }

        void MovementChecks()
        {
            bool islevelOnPlayerSide = (int)_manager.CurrentLevel.ActiveSide == _playerIdIdx;
            bool isPlayerAtEnd = transform.position.x == _manager.CurrentLevelData.MaxXValues[_playerIdIdx];

            if (islevelOnPlayerSide && !isPlayerAtEnd)
            {
                _canMove = true;
            }
            else
            {
                _canMove = false;
            }

            if (isPlayerAtEnd)
            {
                _completedSide = true;
            }
        }
    }
}