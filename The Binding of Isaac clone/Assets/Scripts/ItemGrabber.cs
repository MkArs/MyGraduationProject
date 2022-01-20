using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    public class ItemGrabber : MonoBehaviour
    {
        private string _playerTag = "Player";
        private PlayerController _playerController;
        private Item _item;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _playerTag)
            {
                _playerController = collision.gameObject.GetComponent<PlayerController>();

                if (_playerController.RoomType == RoomType.shop)
                {
                    if (_playerController.CoinsAmount < 5)
                    {
                        return;
                    }
                    else
                    {
                        _playerController.CoinsAmount -= 5;
                    }
                }

                _item = GetComponent<ItemSpawner>().Item;

                GrabItem();

                Destroy(gameObject);

                return;
            }
        }

        public void GrabItem()
        {
            _playerController.HeartContainers += _item.HeartContainersAdded;
            _playerController.Health += _item.HealthAdded;
            _playerController.Damage += _item.DamageAdded;
            _playerController.DamageMultiplier *= _item.DamageMultiplierAdded;
            _playerController.Speed += _item.SpeedAdded;
            _playerController.ShotSpeed += _item.ShotSpeedAdded;
            _playerController.TearDelay -= _item.TearsAdded;
            _playerController.TearPrefab.transform.localScale = new Vector2(
                _playerController.TearPrefab.transform.localScale.x + _item.TearScaleAdded,
                _playerController.TearPrefab.transform.localScale.y + _item.TearScaleAdded
                );
        }
    }
}