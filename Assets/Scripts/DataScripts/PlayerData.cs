namespace BlockyRoad
{
    using UnityEngine;

    /// <summary>
    /// The data class for each Player (North & South)
    /// </summary>
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public enum PlayerSide { North, South };

        /// <summary>
        /// The material which that side represents
        /// </summary>
        public Material PlayerMaterial;

        /// <summary>
        /// The side which the player is part of
        /// </summary>
        // TODO: Add East, West functionality
        public PlayerSide PlayerIdentifier;

        /// <summary>
        /// Whether the player moves in positives or negatives
        /// </summary>
        // TODO: Automate this
        public int XMovement;
    }
}
