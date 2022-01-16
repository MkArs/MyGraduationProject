using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsaacClone
{
    public class BaseBoss : MonoBehaviour
    {
        [SerializeField]
        private float _health = 200f;
        [SerializeField]
        private Slider _hpSlider;
        [SerializeField]
        private GameObject _hpPanel;

        private bool _isInvincibleFromExplosions = false;
        private float _explosionInvincibilityDuration = 0.4f;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;

                if (value <= 0f)
                {
                    HpPanel.SetActive(false);
                    Destroy(gameObject);

                    return;
                }

                _hpSlider.value = value;
            }
        }

        public Slider HpSlider { get => _hpSlider; set => _hpSlider = value; }
        public bool IsInvincibleFromExplosions { get => _isInvincibleFromExplosions; set => _isInvincibleFromExplosions = value; }
        public GameObject HpPanel { get => _hpPanel; set => _hpPanel = value; }

        public IEnumerator BecomeInvincibleFromExplosions()
        {
            _isInvincibleFromExplosions = true;
            yield return new WaitForSeconds(_explosionInvincibilityDuration);
            _isInvincibleFromExplosions = false;
        }

        public void InvincibleCoroutineStarter()
        {
            StartCoroutine(BecomeInvincibleFromExplosions());
        }
    }
}