using System.Collections;
using UnityEngine;

/// <summary>
/// Класс врага ближнего боя
/// </summary>
namespace IsaacClone
{
    public class BaseChaser : BaseEnemy
    {
        [SerializeField]
        private float _speed;

        public float Speed { get => _speed; set => _speed = value; }

        private void Start()
        {
            _soundManager = Camera.main.GetComponent<SoundManager>();

            Player = GameObject.Find("Player");
        }

        private void Update()
        {
            Chase();

            if (_soundManager.ZombieGrawl.isPlaying) return;

            _soundManager.PlayZombieGrawl();
        }

        /// <summary>
        /// Преследовать
        /// </summary>
        public virtual void Chase()
        {
            gameObject.transform.position = (Vector3)Vector2.MoveTowards(gameObject.transform.position, Player.transform.position, _speed * Time.deltaTime);

            if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < AttackRange && IsAttackStarted == false)
            {
                StartCoroutine(Attack());
            }
        }

        /// <summary>
        /// Аттаковать
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator Attack()
        {
            IsAttackStarted = true;

            yield return new WaitForSeconds(AttackDelay);

            if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= AttackRange)
            {
                Player.gameObject.GetComponent<PlayerController>().Health -= 0.5f;
            }

            IsAttackStarted = false;
        }
    }
}