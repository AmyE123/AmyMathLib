namespace BlockyRoad
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public LevelData[] AllLevels;

        public Level CurrentLevel;
        public GameObject CurrentLevelGameObject;
        public LevelData CurrentLevelData;

        public Player[] _players;

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
            if (IsLevelComplete())
            {
                LoadNextLevel(AllLevels[1]);
            }

            PlayersCooledDown();
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
            return true;
        }

        public void LoadNextLevel(LevelData nextLevelData)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                _players[i].ResetPlayerParams();
            }

            CurrentLevelGameObject.SetActive(false);

            CurrentLevelData = nextLevelData;

            CurrentLevelGameObject = Instantiate(CurrentLevelData.LevelPrefab);
            CurrentLevel = CurrentLevelGameObject.GetComponent<Level>();
        }
    }
}