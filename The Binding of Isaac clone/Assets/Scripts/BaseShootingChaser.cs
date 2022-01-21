using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    public class BaseShootingChaser : BaseChaser
    {
        [SerializeField]
        private ShootingChaserAttackDependencyType _attackDependency;
        [SerializeField]
        private GameObject _tearPrefab;
        [SerializeField]
        private float _stopDelayBeforeAttacking;
        [SerializeField]
        private float _shotSpeed;

        private float _attackDelayForTimedToChange;
        private bool _canMove = true;

        public float ShotSpeed { get => _shotSpeed; set => _shotSpeed = value; }

        private void Start()
        {
            _soundManager = Camera.main.GetComponent<SoundManager>();

            Player = GameObject.Find("Player");

            if (_attackDependency == ShootingChaserAttackDependencyType.timed)
            {
                _attackDelayForTimedToChange = AttackDelay;
            }
        }

        private void Update()
        {
            Chase();
        }

        public override void Chase()
        {
            if (_canMove)
            {
                gameObject.transform.position = (Vector3)Vector2.MoveTowards(gameObject.transform.position, Player.transform.position, Speed * Time.deltaTime);
            }

            if (_attackDependency == ShootingChaserAttackDependencyType.ranged)
            {
                if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < AttackRange && IsAttackStarted == false)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                if (IsAttackStarted == false)
                {
                    _attackDelayForTimedToChange -= Time.deltaTime;
                    
                    if (_attackDelayForTimedToChange <= 0f)
                    {
                        _attackDelayForTimedToChange = AttackDelay;

                        StartCoroutine(Attack());
                    }
                }
            }
        }

        public override IEnumerator Attack()
        {
            IsAttackStarted = true;

            if (_stopDelayBeforeAttacking != 0f)
            {
                _canMove = false;
            }

            yield return new WaitForSeconds(_stopDelayBeforeAttacking);

            _soundManager.PlayMonsterCuteGrunt();

            Shoot();

            if (_stopDelayBeforeAttacking != 0f)
            {
                _canMove = true;
            }

            yield return new WaitForSeconds(AttackDelay);

            IsAttackStarted = false;
        }

        public void Shoot()
        {
            GameObject tear = Instantiate(_tearPrefab, transform.position, Quaternion.identity) as GameObject;
            tear.AddComponent<Rigidbody2D>().gravityScale = 0f;
            tear.GetComponent<TearController>().ShotSpeed = _shotSpeed;
        }
    }
}