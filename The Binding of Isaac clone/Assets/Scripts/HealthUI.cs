using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsaacClone
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _healthUISprites;
        [SerializeField]
        private Image[] _healthUIImages;

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = gameObject.GetComponent<PlayerController>();
        }

        public void ChangeHPUI()
        {
            int paintFullNumber = Mathf.FloorToInt(_playerController.Health);
            int paintHalfNumber = 0;
            int emptyHeartContainers = 0;

            if (_playerController.Health - (float)paintFullNumber != 0f)
            {
                paintHalfNumber = 1;
            }

            emptyHeartContainers = (int)_playerController.HeartContainers - (paintFullNumber + paintHalfNumber);

            for (int i = 0; i < paintFullNumber; i++)
            {
                _healthUIImages[i].sprite = _healthUISprites[3];
            }

            for (int i = paintFullNumber; i < paintFullNumber + paintHalfNumber; i++)
            {
                _healthUIImages[i].sprite = _healthUISprites[2];
            }

            for (int i = paintFullNumber + paintHalfNumber; i < paintFullNumber + paintHalfNumber + emptyHeartContainers; i++)
            {
                _healthUIImages[i].sprite = _healthUISprites[1];
            }

            for (int i = paintFullNumber + paintHalfNumber + emptyHeartContainers; i < _healthUIImages.Length; i++)
            {
                _healthUIImages[i].sprite = _healthUISprites[0];
            }
        }
    }
}