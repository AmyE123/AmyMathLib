namespace BlockyRoad
{
    using UnityEngine;

    public class PlayerSquares : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _squares;

        [SerializeField]
        private GameManager _manager;

        [SerializeField]
        private Player _player;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();
            _player = GetComponentInParent<Player>();
        }

        void Update()
        {
            if (_manager.PlayerCharge < 1 || !_player.PlayerCanTakeTurn)
            {
                for (int i = 0; i < _squares.Length; i++)
                {
                    _squares[i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (_manager.PlayerCharge == 1)
                {
                    _squares[0].gameObject.SetActive(true);
                    _squares[1].gameObject.SetActive(false);
                    _squares[2].gameObject.SetActive(false);
                }
                else if (_manager.PlayerCharge == 2)
                {
                    _squares[0].gameObject.SetActive(true);
                    _squares[1].gameObject.SetActive(true);
                    _squares[2].gameObject.SetActive(false);
                }
                else
                {
                    _squares[0].gameObject.SetActive(true);
                    _squares[1].gameObject.SetActive(true);
                    _squares[2].gameObject.SetActive(true);
                }

            }
        }
    }

}