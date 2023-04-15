namespace BlockyRoad
{
    using UnityEngine;

    public class Level : MonoBehaviour
    {
        public enum Sides { North, South };

        public Sides ActiveSide = Sides.North;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ActiveSide == Sides.North)
                {
                    Debug.Log("[LEVEL] Turning North -> South");
                    ActiveSide = Sides.South;

                    transform.Rotate(180, 0, 0);
                }
                else
                {
                    Debug.Log("[LEVEL] Turning South -> North");
                    ActiveSide = Sides.North;

                    transform.Rotate(-180, 0, 0);
                }
            }
        }
    }
}