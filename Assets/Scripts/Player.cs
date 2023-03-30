namespace BlockyRoad
{
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
                transform.position = new Vector3(transform.position.x + xMov, transform.position.y, transform.position.z);
            }
        }

        void Update()
        {
            if ((int)_manager.CurrentLevel.ActiveSide == _playerIdIdx)
            {               
                _canMove = true;
                Debug.Log($"{gameObject.name} Can Move = True");
            }
            else
            {
                _canMove = false;
                Debug.Log($"{gameObject.name} Can Move = False");
            }

            if (_manager.CurrentLevelGameObject != null && !_parentSet)
            {
                SetPlayerLevelParent();
            }

            PlayerMovement();
        }
    }
}