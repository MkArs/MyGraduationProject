using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Монстро
    /// </summary>
    public class Monstro : BaseBoss
    {
        private GameObject _player;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _playerPosition;
        private bool _isMidJump = false;
        private float _directionCoefficient = 1f;

        [SerializeField]
        private float _timeBeforeLongJump = 0.5f;
        [SerializeField]
        private float _goUpAndDownLongJumpSpeed = 10f;
        [SerializeField]
        private float _longJumpSpeed = 5f;
        [SerializeField]
        private float _longJumpCeiling = 4f;
        [SerializeField]
        private CircleCollider2D _collider;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player");
            _playerPosition = _player.transform.localPosition;
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            HpPanel = GameObject.Find("Canvas").GetComponent<BaseBoss>().HpPanel;
            HpPanel.SetActive(true);
            HpSlider = GameObject.Find("Canvas").GetComponent<BaseBoss>().HpSlider;
            HpSlider.enabled = true;
            HpSlider.maxValue = Health;
            HpSlider.value = Health;

            _longJumpCeiling = _playerPosition.y + _longJumpCeiling;
        }

        // Update is called once per frame
        void Update()
        {
            LookAtPlayer();

            if (_isMidJump == false)
            {
                StartCoroutine(MakeLongJump());
            }
        }
         
        /// <summary>
        /// Посмотреть на игрока
        /// </summary>
        public void LookAtPlayer()
        {
            if (_player.gameObject.transform.localPosition.x > gameObject.transform.localPosition.x)
            {
                _spriteRenderer.flipX = true;
                return;
            }

            _spriteRenderer.flipX = false;
        }

        /// <summary>
        /// Сделать длинный прыжок
        /// </summary>
        /// <returns></returns>
        public IEnumerator MakeLongJump()
        {
            _isMidJump = true;

            yield return new WaitForSeconds(_timeBeforeLongJump);

            _collider.enabled = false;

            _playerPosition = _player.transform.localPosition;

            while (gameObject.transform.localPosition.y < _longJumpCeiling)
            {
                transform.localPosition += Vector3.up * _goUpAndDownLongJumpSpeed * Time.deltaTime;

                yield return new WaitForSeconds(0.01f);
            }

            transform.localPosition = new Vector2(transform.localPosition.x, _longJumpCeiling);

            if (_playerPosition.x >= transform.localPosition.x)
            {
                _directionCoefficient = 1f;
            }
            else
            {
                _directionCoefficient = -1f;
            }

            while (Mathf.Abs(gameObject.transform.localPosition.x - _playerPosition.x) > 0.01f)
            {
                transform.localPosition += Vector3.right * _directionCoefficient * _longJumpSpeed * Time.deltaTime;

                yield return new WaitForSeconds(0.005f);
            }

            while (gameObject.transform.localPosition.y > _playerPosition.y)
            {
                transform.position = Vector2.MoveTowards(
                    transform.localPosition,
                    _playerPosition,
                    _goUpAndDownLongJumpSpeed * Time.deltaTime
                    );

                yield return new WaitForSeconds(0.01f);
            }

            gameObject.transform.localPosition = _playerPosition;

            _isMidJump = false;

            _collider.enabled = true;
        }
    }
}