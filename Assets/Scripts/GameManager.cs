namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        const int MOUSE_CHARGE_MAX = 4;

        public LevelData[] AllLevels;

        public Level CurrentLevel;
        public GameObject CurrentLevelGameObject;
        public LevelData CurrentLevelData;

        public Player[] _players;

        private float _levelReloadTimer = 2f;

        [SerializeField]
        private float _mouseCharge = 0;

        [SerializeField]
        private int _playerCharge = 0;

        public int PlayerCharge => _playerCharge;
        public int TrueCharge => (int)_mouseCharge;
        public int MaxCharge => MOUSE_CHARGE_MAX - 1;

        private void InstantiateLevel()
        {
            CurrentLevelGameObject = Instantiate(CurrentLevelData.LevelPrefab);
            CurrentLevel = CurrentLevelGameObject.GetComponent<Level>();
        }

        private void Start()
        {
            InstantiateLevel();
            
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && _mouseCharge < MOUSE_CHARGE_MAX)
            {
                _playerCharge = 0;
                _mouseCharge += Time.deltaTime;
                _playerCharge = AMaths.Round(_mouseCharge);
            }
            else
            {
                _mouseCharge = 0;                
            }
        }

        public bool PlayersCooledDown()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].HasCooledDown == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsLevelComplete()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].HasCompletedSide == false)
                {
                    return false;
                }
            }

            Debug.Log("[LEVEL] Level complete!");

            _levelReloadTimer -= Time.deltaTime;

            if (_levelReloadTimer <= 0)
            {                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);                
            }
            
            return true;
        }
    }
}