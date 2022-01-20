using UnityEngine;

namespace IsaacClone
{
    public class PickUpController : MonoBehaviour
    {
        [SerializeField]
        private PickUpType _pickUp;
        [SerializeField]
        private float _quantity;

        private string _playerTag = "Player";

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _playerTag)
            {
                switch (_pickUp)
                {
                    case PickUpType.coin:
                        collision.gameObject.GetComponentInParent<PlayerController>().CoinsAmount += (int)_quantity;

                        if (gameObject.name.ToLower().Contains("lucky"))
                        {
                            collision.gameObject.GetComponentInParent<PlayerController>().Luck++;
                        }

                        break;
                    case PickUpType.key:
                        collision.gameObject.GetComponentInParent<PlayerController>().KeysAmount += (int)_quantity;
                        break;
                    case PickUpType.bomb:
                        collision.gameObject.GetComponentInParent<PlayerController>().BombsAmount += (int)_quantity;
                        break;
                    case PickUpType.heart:
                        if (collision.gameObject.GetComponentInParent<PlayerController>().Health == collision.gameObject.GetComponentInParent<PlayerController>().HeartContainers)
                        {
                            return;
                        }

                        collision.gameObject.GetComponentInParent<PlayerController>().Health += (int)_quantity;
                        break;
                }

                Destroy(gameObject);

                return;
            }
        }
    }
}