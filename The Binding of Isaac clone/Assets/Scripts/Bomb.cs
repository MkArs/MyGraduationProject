using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Бомба
    /// </summary>
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _explosionSprites;

        private PlayerController _playerController;
        private Rigidbody2D _rigidBody;
        private CircleCollider2D _circleCollider2D;
        private SpriteRenderer _spriteRenderer;
        private bool _didExposionHappen = false;

        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); ;
            _rigidBody = gameObject.GetComponent<Rigidbody2D>();
            _circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            StartCoroutine(Explode());
        }

        /// <summary>
        /// Взрываться
        /// </summary>
        /// <returns></returns>
        public IEnumerator Explode()
        {
            float count = 0f;
            float gColor = 0f;

            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            var timeBeforeBombExplosion = _playerController.TimeBeforeBombExplosion;

            Color blinkColor = spriteRenderer.color;

            while (count < timeBeforeBombExplosion)
            {
                count += 0.1f;
                yield return new WaitForSeconds(0.1f);

                blinkColor.g = gColor;

                spriteRenderer.color = blinkColor;

                if (gColor == 0f)
                {
                    gColor = 255f;
                }
                else
                {
                    gColor = 0f;
                }
            }

            _rigidBody.mass = 1000f;

            _circleCollider2D.radius = 0.64f;

            _didExposionHappen = true;

            int spriteNumber = 0;

            Camera.main.GetComponent<SoundManager>().PlayBombExplodes();

            while (spriteNumber < _explosionSprites.Length)
            {
                _spriteRenderer.sprite = _explosionSprites[spriteNumber];

                spriteNumber++;

                yield return new WaitForSeconds(0.04f);
            }

            Destroy(gameObject);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_didExposionHappen)
            {
                if (collision.gameObject.name.ToLower().Contains("rock"))
                {
                    Destroy(collision.gameObject);

                    Camera.main.GetComponent<SoundManager>().PlayRockCrumble();

                    return;
                }

                if (collision.gameObject.tag == "Player" && _playerController.IsInvincible == false)
                {
                    _playerController.Health -= 0.5f;

                    return;
                }

                if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<BaseEnemy>().IsInvincibleFromExplosions == false)
                {
                    var baseEnemy = collision.gameObject.GetComponent<BaseEnemy>();
                    baseEnemy.Health -= _playerController.ExplosionDamage;
                    baseEnemy.InvincibleCoroutineStarter();

                    return;
                }

                if (collision.gameObject.tag == "Boss" && collision.gameObject.GetComponent<BaseBoss>().IsInvincibleFromExplosions == false)
                {
                    var baseBoss = collision.gameObject.GetComponent<BaseBoss>();
                    baseBoss.Health -= _playerController.ExplosionDamage;
                    baseBoss.InvincibleCoroutineStarter();
                }
            }
        }
    }
}