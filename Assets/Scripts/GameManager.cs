namespace BlockyRoad
{
    using System.Linq;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
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
            IsLevelComplete();
        }

        private bool IsLevelComplete()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].HasCompletedSide == false)
                {
                    return false;
                }
            }

            Debug.Log("AE: Level complete!");
            return true;
        }
    }
}