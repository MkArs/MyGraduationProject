using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IsaacClone
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Text _collectedText;
        [SerializeField]
        private GameObject _tearPrefab;
        [SerializeField]
        private float _shotSpeed;
        [SerializeField]
        private float _tearDelay;
        [SerializeField]
        private float _range;
        [SerializeField]
        private Transform _head;
        [SerializeField]
        private float _health = 3f;
        [SerializeField]
        private float _invincibilityDuration = 1f;
        [SerializeField]
        private float _damage = 3.5f;

        private Rigidbody2D _rigidbody;
        private float _lastTear;
        private int _collectedAmount = 0;
        private bool _isInvincible = false;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (_health > value)
                {
                    if (value == 0f)
                    {
                        Debug.Log("I AM DEAD");
                    }
                    else
                    {
                        StartCoroutine(BecomeInvincible());
                    }
                }

                _health = value;
            }
        }

        public int CollectedAmount { get => _collectedAmount; set => _collectedAmount = value; }
        public float Range { get => _range; set => _range = value; }
        public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
        public float Damage { get => _damage; set => _damage = value; }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float shootHorizontal = Input.GetAxis("ShootHorizontal");
            float shootVertical = Input.GetAxis("ShootVertical");

            if ((shootHorizontal != 0f || shootVertical != 0f) && Time.time > _lastTear + _tearDelay)
            {
                Shoot(shootHorizontal, shootVertical);
                _lastTear = Time.time;
            }

            _rigidbody.velocity = new Vector2(horizontal * _speed, vertical * _speed);
            _collectedText.text = "Items collected: " + CollectedAmount;
        }

        /// <summary>
        /// Стрелять
        /// </summary>
        /// <param name="x">Направление выстрела по Х</param>
        /// <param name="y">Направление выстрела по У</param>
        public void Shoot(float x, float y)
        {
            GameObject tear = Instantiate(_tearPrefab, _head.transform.position, _head.transform.rotation) as GameObject;
            tear.AddComponent<Rigidbody2D>().gravityScale = 0f;
            tear.GetComponent<Rigidbody2D>().velocity = new Vector2(
                (x < 0f) ? Mathf.Floor(x) * _shotSpeed : Mathf.Ceil(x) * _shotSpeed,
                (y < 0f) ? Mathf.Floor(y) * _shotSpeed : Mathf.Ceil(y) * _shotSpeed
            );
        }

        /// <summary>
        /// Стать неуязвимым
        /// </summary>
        /// <returns></returns>
        private IEnumerator BecomeInvincible()
        {
            float count = 0f;
            float aColor = 0.3f;
            var spriteRendererArray = gameObject.GetComponentsInChildren<SpriteRenderer>();

            _isInvincible = true;

            Color blinkColor = spriteRendererArray[0].color;

            _isInvincible = true;

            while (count < _invincibilityDuration)
            {
                count += 0.1f;
                yield return new WaitForSeconds(0.1f);

                blinkColor.a = aColor;

                ChangePlayersColor(blinkColor, spriteRendererArray);

                if (aColor == 0.3f)
                {
                    aColor = 1f;
                }
                else
                {
                    aColor = 0.3f;
                }
            }

            blinkColor.a = 1f;

            ChangePlayersColor(blinkColor, spriteRendererArray);

            _isInvincible = false;
        }

        /// <summary>
        /// Изменить цвет игрока
        /// </summary>
        /// <param name="blinkColor">Цвет подмены</param>
        /// <param name="spriteRendererArray">Массив рендереров, цвета которых надо перекрасить</param>
        public void ChangePlayersColor(Color blinkColor, SpriteRenderer[] spriteRendererArray)
        {
            foreach (var spriteRenderer in spriteRendererArray)
            {
                spriteRenderer.color = blinkColor;
            }
        }
    }
}