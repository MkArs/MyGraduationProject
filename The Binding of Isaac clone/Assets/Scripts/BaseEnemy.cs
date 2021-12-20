using UnityEngine;

/// <summary>
/// Класс базового врага
/// </summary>
namespace IsaacClone
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        private float _health;
        [SerializeField]
        private float _attackDelay;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _attackRange;

        private GameObject _player;
        private bool _isAttackStarted = false;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value <= 0f)
                {
                    Die();
                }

                _health = value;
            }
        }

        public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }
        public float AttackRange { get => _attackRange; set => _attackRange = value; }
        public GameObject Player { get => _player; set => _player = value; }
        public bool IsAttackStarted { get => _isAttackStarted; set => _isAttackStarted = value; }

        /// <summary>
        /// Умирать
        /// </summary>
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}