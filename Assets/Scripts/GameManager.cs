namespace BlockyRoad
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public LevelData[] AllLevels;

        public Level CurrentLevel;
        public GameObject CurrentLevelGameObject;
        public LevelData CurrentLevelData;

        public Player[] _players;

        private float _levelReloadTimer = 2f;

        private void InstantiateLevel()
        {
            CurrentLevelGameObject = Instantiate(CurrentLevelData.LevelPrefab);
            CurrentLevel = CurrentLevelGameObject.GetComponent<Level>();
        }

        private void Start()
        {
            InstantiateLevel();
            
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