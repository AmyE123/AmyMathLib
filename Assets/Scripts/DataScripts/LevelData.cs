namespace BlockyRoad
{
    using UnityEngine;

    /// <summary>
    /// The data class for each level
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        /// <summary>
        /// The index of the level
        /// </summary>
        public int LevelIdx;

        /// <summary>
        /// The level prefab that would load with the level
        /// </summary>
        public GameObject LevelPrefab;

        /// <summary>
        /// The vectors where each player starts (N, S)
        /// </summary>
        public Vector3[] PlayersStartPos;

        /// <summary>
        /// The max X value which the player can reach.
        /// If they get here that means they have completed
        /// the level as they would be on the button.
        /// </summary>
        //TODO: Implement auto-detection of level-end
        public float[] MaxXValues;

        /// <summary>
        /// The location of the blockers located within the level
        /// </summary>
        //TODO: Implement auto-detection of blockers
        public Vector2[] LevelBlockers;


        /// <summary>
        /// The location of the buttons located within the level
        /// </summary>
        //TODO: Implement auto-detection of buttons
        public Vector2[] LevelButtons;
    }
}