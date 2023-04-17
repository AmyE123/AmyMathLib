namespace BlockyRoad
{
    using UnityEngine;

    public class Level : MonoBehaviour
    {
        [SerializeField]
        private GameManager _manager;

        public enum Sides { North, South };

        public Sides ActiveSide = Sides.North;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _manager.PlayersCooledDown())
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