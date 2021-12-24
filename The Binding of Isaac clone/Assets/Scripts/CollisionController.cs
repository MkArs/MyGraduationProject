using UnityEngine;

namespace IsaacClone
{
    public class CollisionController : MonoBehaviour
    {
        private string _enemyTag = "Enemy";
        private string _playerTag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.ToLower().Contains("tear") && gameObject.tag == _enemyTag && collision.gameObject.GetComponent<TearController>().TearSource == TearSourceType.player)
            {
                gameObject.GetComponent<BaseEnemy>().Health -= GameObject.Find("Player").GetComponent<PlayerController>().Damage * 
                    GameObject.Find("Player").GetComponent<PlayerController>().DamageMultiplier;
                Destroy(collision.gameObject);
                return;
            }

            if (collision.name.ToLower().Contains("tear") && gameObject.tag == _playerTag 
                && collision.gameObject.GetComponent<TearController>().TearSource == TearSourceType.enemy
                && gameObject.GetComponent<PlayerController>().IsInvincible == false)
            {
                gameObject.GetComponent<PlayerController>().Health -= 0.5f;
                Destroy(collision.gameObject);
                return;
            }
        }

        /// <summary>
        /// Обработчик удерживания на коллизии
        /// </summary>
        /// <param name="collision"></param>
        /// </remarks>Используется вместо OnCollisionEnter, чтобы игрок вновь получал урон, упираясь во врага.<remarks>
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (gameObject.tag == _enemyTag && collision.gameObject.tag == _playerTag && collision.gameObject.GetComponent<PlayerController>().IsInvincible == false)
            {
                collision.gameObject.GetComponentInParent<PlayerController>().Health -= 0.5f;
            }
        }
    }
}
