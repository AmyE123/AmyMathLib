namespace BlockyRoad
{
    using UnityEngine;

    public class BlockingCube : MonoBehaviour
    {
        public MeshManipulation MeshMani;

        public void ShrinkWall()
        {
            MeshMani.LerpScaleObject(0.1f, 0.1f, 1, 1f);
        }
    }
}