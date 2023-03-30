namespace BlockyRoad
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public int LevelIdx;
        public GameObject LevelPrefab;
        public Vector3[] PlayersStartPos;
        public float[] MaxXValues;
    }
}