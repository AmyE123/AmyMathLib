namespace BlockyRoad
{
    using UnityEngine;

    /// <summary>
    /// [WIP] A class for the player movement squares when using charge movement
    /// </summary>
    public class PlayerSquares : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The movement squares")]
        private Transform[] _squares;

        [SerializeField]
        [Tooltip("The manager for the game")]
        private GameManager _manager;

        [SerializeField]
        [Tooltip("The player which the movement is attached to")]
        private Player _player;

        private void Start()
        {
            _manager = FindObjectOfType<GameManager>();
            _player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            //TODO: BAD implementation as this is just a test, can use for loop
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