using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Отвечает за отдельную дверь
    /// </summary>
    public class DoorComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _DoorOpener;
        [SerializeField]
        private GameObject _AdjacentDoor;

        /// <summary>
        /// Открыть или закрыть дверь
        /// </summary>
        /// <param name="isDoorOpened">Нужно ли закрывать дверь</param>
        public void OpenOrCloseDoor(bool isDoorOpened)
        {
            _DoorOpener.SetActive(isDoorOpened);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (_AdjacentDoor.name.ToLower().Contains("lower"))
                {
                    collision.gameObject.transform.position = new Vector2(_AdjacentDoor.transform.position.x, _AdjacentDoor.transform.position.y + 0.2f);
                }
                else if (_AdjacentDoor.name.ToLower().Contains("upper"))
                {
                    collision.gameObject.transform.position = new Vector2(_AdjacentDoor.transform.position.x, _AdjacentDoor.transform.position.y - 0.2f);
                }
                else if (_AdjacentDoor.name.ToLower().Contains("right"))
                {
                    collision.gameObject.transform.position = new Vector2(_AdjacentDoor.transform.position.x - 0.2f, _AdjacentDoor.transform.position.y);
                }
                else if (_AdjacentDoor.name.ToLower().Contains("left"))
                {
                    collision.gameObject.transform.position = new Vector2(_AdjacentDoor.transform.position.x + 0.2f, _AdjacentDoor.transform.position.y);
                }
            }
        }
    }
}