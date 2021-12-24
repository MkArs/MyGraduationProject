using System;
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
        private Text _txtCoinsAmount;
        [SerializeField]
        private Text _txtKeysAmount;
        [SerializeField]
        private Text _txtBombsAmount;
        [SerializeField]
        private GameObject _tearPrefab;
        [SerializeField]
        private GameObject _bombPrefab;
        [SerializeField]
        private float _shotSpeed;
        [SerializeField]
        private float _tearDelay;
        [SerializeField]
        private float _bombDelay = 2f;
        [SerializeField]
        private float _range;
        [SerializeField]
        private Transform _head;
        [SerializeField]
        private float _health = 3f;
        [SerializeField]
        private float _heartContainers = 3f;
        [SerializeField]
        private float _invincibilityDuration = 1f;
        [SerializeField]
        private float _damage = 3.5f;
        [SerializeField]
        private float _damageMultiplier = 1f;
        [SerializeField]
        private float _explosionDamage = 75f;
        [SerializeField]
        private float _timeBeforeBombExplosion = 2f;

        private const int _maxBombsAmount = 99;
        private const int _maxKeysAmount = 99;
        private const int _maxCoinsAmount = 99;
        private const float _maxHeartContainers = 12f;

        private int _bombsAmount = 0;
        private int _keysAmount = 0;
        private int _coinsAmount = 0;
        private Rigidbody2D _rigidbody;
        private HealthUI _healthUI;
        private float _lastTear;
        private float _lastBomb;
        private bool _isInvincible = false;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value >= _heartContainers)
                {
                    _health = _heartContainers;
                    _healthUI.ChangeHPUI();
                    return;
                }

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

                _healthUI.ChangeHPUI();
            }
        }

        public float Range { get => _range; set => _range = value; }
        public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
        public float Damage { get => _damage; set => _damage = value; }

        public int BombsAmount {
            get
            {
                return _bombsAmount;
            }
            set
            {
                if (value < _maxBombsAmount)
                {
                    _bombsAmount = value;
                    AddAndShowPickupAmount(_bombsAmount, _txtBombsAmount);
                }
                else
                {
                    _bombsAmount = _maxBombsAmount;
                    AddAndShowPickupAmount(_bombsAmount, _txtBombsAmount);
                }
            }
        }

        public int KeysAmount {
            get
            {
                return _keysAmount;
            }
            set
            {
                if (value < _maxKeysAmount)
                {
                    _keysAmount = value;
                    AddAndShowPickupAmount(_keysAmount, _txtKeysAmount);
                }
                else
                {
                    _keysAmount = _maxKeysAmount;
                    AddAndShowPickupAmount(_keysAmount, _txtKeysAmount);
                }
            }
        }

        public int CoinsAmount {
            get
            {
                return _coinsAmount;
            }
            set
            {
                if (value < _maxCoinsAmount)
                {
                    _coinsAmount = value;
                    AddAndShowPickupAmount(_coinsAmount, _txtCoinsAmount);
                }
                else
                {
                    _coinsAmount = _maxCoinsAmount;
                    AddAndShowPickupAmount(_coinsAmount, _txtCoinsAmount);
                }
            }
        }

        public float TimeBeforeBombExplosion { get => _timeBeforeBombExplosion; set => _timeBeforeBombExplosion = value; }
        public float ExplosionDamage { get => _explosionDamage; set => _explosionDamage = value; }
        public float HeartContainers {
            get
            {
                return _heartContainers;
            }
            set
            {
                if (value > _maxHeartContainers) return;

                _heartContainers = value;
                _healthUI.ChangeHPUI();
            }
        }
        public float DamageMultiplier { get => _damageMultiplier; set => _damageMultiplier = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float ShotSpeed { get => _shotSpeed; set => _shotSpeed = value; }
        public float TearDelay { get => _tearDelay; set => _tearDelay = value; }
        public GameObject TearPrefab { get => _tearPrefab; set => _tearPrefab = value; }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthUI = GetComponent<HealthUI>();

            _tearPrefab.transform.localScale = new Vector2(0.05f, 0.05f);
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _lastBomb + _bombDelay && _bombsAmount > 0)
            {
                GameObject bombPrefab = Instantiate(_bombPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                _lastBomb = Time.time;
                _bombsAmount--;
                AddAndShowPickupAmount(_bombsAmount, _txtBombsAmount);
            }
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

        public void AddAndShowPickupAmount(int pickUpAmount, Text txtPickUp)
        {
            string amount = Convert.ToString(pickUpAmount);

            if (amount.Length == 1) amount = "0" + amount;

            txtPickUp.text = amount;
        }
    }
}
