namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// The main game manager, responsible for managing game behaviour
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        const int MOUSE_CHARGE_MAX = 4;

        private float _levelReloadTimer = 2f;

        [SerializeField]
        [Tooltip("The amount the mouse has charged up when held down")]
        private float _mouseCharge = 0;

        [SerializeField]
        [Tooltip("The rounded amount the mouse has charged up when held down, for the player to move multiple blocks")]
        private int _playerCharge = 0;

        /// <summary>
        /// All levels avaliable
        /// </summary>
        public LevelData[] AllLevels;

        /// <summary>
        /// The current level the game is on
        /// </summary>
        public Level CurrentLevel;

        /// <summary>
        /// The prefab of the current level
        /// </summary>
        public GameObject CurrentLevelGameObject;

        /// <summary>
        /// The level data of the current level
        /// </summary>
        public LevelData CurrentLevelData;

        /// <summary>
        /// An array of all players in the level
        /// </summary>
        public Player[] _players;

        /// <summary>
        /// The player's movement charge
        /// </summary>
        public int PlayerCharge => _playerCharge;

        /// <summary>
        /// The true charge based off _mouseCharge
        /// </summary>
        public int TrueCharge => (int)_mouseCharge;

        /// <summary>
        /// The max charge of movement the player can have
        /// </summary>
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

        /// <summary>
        /// Checks for player cool down
        /// </summary>
        /// <returns>A boolean indicating whether the player has cooled down</returns>
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

        /// <summary>
        /// Checks for level completion
        /// </summary>
        /// <returns>A boolean indicating if all players have reached the end of the level</returns>
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