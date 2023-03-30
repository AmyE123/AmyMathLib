namespace BlockyRoad
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public Level CurrentLevel;
        public GameObject CurrentLevelGameObject;
        public LevelData CurrentLevelData; 

        private void InstantiateLevel()
        {
            CurrentLevelGameObject = Instantiate(CurrentLevelData.LevelPrefab);
            CurrentLevel = CurrentLevelGameObject.GetComponent<Level>();
        }

        private void Start()
        {
            InstantiateLevel();
        }
    }
}