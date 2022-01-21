using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    public class RoomEventComponent : MonoBehaviour
    {
        [SerializeField]
        private RoomType _roomType;
        [SerializeField]
        private RoomComponent _roomComponent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Time.time <= 0.1f) return;

            GameObject.Find("Player").GetComponent<PlayerController>().RoomType = _roomType;

            if (collision.tag.ToLower().Contains("player"))
            {
                StartCoroutine(Camera.main.GetComponent<CameraOperator>().MoveCamera(transform.parent));

                switch (_roomType)
                {
                    case RoomType.usual:
                        _roomComponent.SpawnEnemies();
                        break;

                    case RoomType.boss:
                        _roomComponent.SpawnEnemies();
                        Camera.main.GetComponent<SoundManager>().PlayBossTheme();
                        break;

                    case RoomType.treasure:
                        if (_roomComponent.IsRoomCleared == false) Camera.main.GetComponent<SoundManager>().PlayTreasureRoomEnter();
                        _roomComponent.SpawnItem();
                        break;

                    case RoomType.shop:
                        _roomComponent.SpawnItem();
                        break;
                }
            }
        }
    }
}