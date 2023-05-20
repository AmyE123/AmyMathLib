namespace BlockyRoad
{
    using UnityEngine;

    /// <summary>
    /// A blocking cube used within the level
    /// </summary>
    public class BlockingCube : MonoBehaviour
    {
        public MeshManipulation MeshManipulator;

        /// <summary>
        /// Shrinks the wall so the player can get past
        /// </summary>
        public void ShrinkWall()
        {
            MeshManipulator.LerpScaleObject(0.1f, 0.1f, 1, 1f);
        }
    }
}