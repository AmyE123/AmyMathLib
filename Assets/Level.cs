namespace BlockyRoad
{
    using UnityEngine;

    public class Level : MonoBehaviour
    {
        public enum Sides { North, South };

        public Sides ActiveSide = Sides.North;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (ActiveSide == Sides.North) 
                {
                    Debug.Log("Turning North -> South");
                    ActiveSide = Sides.South; 
                }
                else 
                {
                    Debug.Log("Turning South -> North");
                    ActiveSide = Sides.North; 
                }
            }
        }
    }
}