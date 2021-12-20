using UnityEngine;

namespace IsaacClone
{
    public class CollisionController : MonoBehaviour
    {
        private string _enemyTag = "Enemy";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponentInParent<PlayerController>().CollectedAmount++;
                Destroy(gameObject);
                return;
            }

            if (collision.name.ToLower().Contains("tear") && gameObject.tag == _enemyTag && collision.gameObject.GetComponent<TearController>().TearSource == TearSourceType.player)
            {
                gameObject.GetComponent<BaseChaser>().Health -= GameObject.Find("Player").GetComponent<PlayerController>().Damage;
                Destroy(collision.gameObject);
                return;
            }

            if (collision.name.ToLower().Contains("tear") && gameObject.tag == "Player" && collision.gameObject.GetComponent<TearController>().TearSource == TearSourceType.enemy)
            {
                gameObject.GetComponent<PlayerController>().Health -= 0.5f;
                Destroy(collision.gameObject);
            }
        }

        /// <summary>
        /// Обработчик удерживания на коллизии
        /// </summary>
        /// <param name="collision"></param>
        /// </remarks>Используется вместо OnCollisionEnter, чтобы игрок вновь получал урон, упираясь во врага.<remarks>
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (gameObject.tag == _enemyTag && collision.gameObject.GetComponentInParent<PlayerController>().IsInvincible == false)
            {
                collision.gameObject.GetComponentInParent<PlayerController>().Health -= 0.5f;
            }
        }
    }
}
