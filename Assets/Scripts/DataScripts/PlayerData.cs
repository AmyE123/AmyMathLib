namespace BlockyRoad
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public enum PlayerSide { North, South };

        public Material PlayerMaterial;
        public PlayerSide PlayerIdentifier;
        public int XMovement;
    }
}
