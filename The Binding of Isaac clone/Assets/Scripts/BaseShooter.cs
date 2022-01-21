using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Класс врага стрелка
    /// </summary>
    public class BaseShooter : BaseEnemy
    {
        [SerializeField]
        private GameObject _tearPrefab;
        [SerializeField]
        private float _shotSpeed;

        public float ShotSpeed { get => _shotSpeed; set => _shotSpeed = value; }

        private void Start()
        {
            Player = GameObject.Find("Player");

            _soundManager = Camera.main.GetComponent<SoundManager>();
        }

        private void Update()
        {
            LookForPlayer();
        }

        /// <summary>
        /// Искать врага
        /// </summary>
        public void LookForPlayer()
        {
            if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < AttackRange && IsAttackStarted == false)
            {
                StartCoroutine(Attack());
            }
        }

        /// <summary>
        /// Атаковать
        /// </summary>
        /// <returns></returns>
        public IEnumerator Attack()
        {
            IsAttackStarted = true;

            _soundManager.PlayMonsterGrunt();

            Shoot();
            
            yield return new WaitForSeconds(AttackDelay);

            IsAttackStarted = false;
        }

        /// <summary>
        /// Стрелять
        /// </summary>
        public void Shoot()
        {       
            GameObject tear = Instantiate(_tearPrefab, transform.position, Quaternion.identity) as GameObject;
            tear.AddComponent<Rigidbody2D>().gravityScale = 0f;
            tear.GetComponent<TearController>().ShotSpeed = _shotSpeed;
        }
    }
}