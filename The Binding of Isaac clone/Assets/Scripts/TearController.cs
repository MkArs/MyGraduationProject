using UnityEngine;

namespace IsaacClone
{
    public class TearController : MonoBehaviour
    {
        private GameObject _player;
        private float _travelDistance = 0f;
        private Vector2 _startPosition;
        private Vector2 _lastPosition;
        private Vector2 _playerPosition;
        [SerializeField]
        private TearSourceType _tearSource;

        public TearSourceType TearSource { get => _tearSource; set => _tearSource = value; }

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player");
            _startPosition = gameObject.transform.position;

            _playerPosition = _player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_tearSource == TearSourceType.enemy)
            {
                _startPosition = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, _playerPosition, 5f * Time.deltaTime);

                if (_startPosition == _lastPosition)
                {
                    Destroy(gameObject);
                }

                _lastPosition = _startPosition;

                return;
            }

            if (_tearSource == TearSourceType.player)
            {
                _travelDistance += Vector2.Distance(_startPosition, gameObject.transform.position);
                _startPosition = gameObject.transform.position;

                if (_travelDistance >= _player.gameObject.GetComponent<PlayerController>().Range)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
