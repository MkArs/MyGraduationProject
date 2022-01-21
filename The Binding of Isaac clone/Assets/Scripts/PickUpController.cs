using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Отвечает за подбор одноразовых предметов 
    /// </summary>
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
                        if (gameObject.name.ToLower().Contains("penny"))
                        {
                            Camera.main.GetComponent<SoundManager>().PlayPennyGrab();
                        }
                        else if (gameObject.name.ToLower().Contains("nickel"))
                        {
                            Camera.main.GetComponent<SoundManager>().PlayNickelGrab();
                        }
                        else if (gameObject.name.ToLower().Contains("dime"))
                        {
                            Camera.main.GetComponent<SoundManager>().PlayDimeGrab();
                        }

                        collision.gameObject.GetComponentInParent<PlayerController>().CoinsAmount += (int)_quantity;

                        if (gameObject.name.ToLower().Contains("lucky"))
                        {
                            collision.gameObject.GetComponentInParent<PlayerController>().Luck++;
                        }

                        break;

                    case PickUpType.key:
                        Camera.main.GetComponent<SoundManager>().PlayKeyGrab();

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

                        Camera.main.GetComponent<SoundManager>().PlayHPGrab();

                        collision.gameObject.GetComponentInParent<PlayerController>().Health += (int)_quantity;
                        break;
                }

                Destroy(gameObject);

                return;
            }
        }
    }
}