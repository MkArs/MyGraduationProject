using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    [CreateAssetMenu(menuName = "Items", fileName = "New Item")]
    public class Item : ScriptableObject
    {
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private float _heartContainersAdded;
        [SerializeField]
        private float _healthAdded;
        [SerializeField]
        private float _tearsAdded;
        [SerializeField]
        private float _shotSpeedAdded;
        [SerializeField]
        private float _speedAdded;
        [SerializeField]
        private float _tearScaleAdded;
        [SerializeField]
        private float _rangeAdded;
        [SerializeField]
        private float _damageAdded;
        [SerializeField]
        private float _damageMultiplierAdded;
        [SerializeField] [Range(0,4)]
        private int _itemQuality;
        [SerializeField]
        private string _itemDescription;
        [SerializeField]
        private string _itemID;
        [SerializeField]
        private TearEffectType[] _tearEffects;
        [SerializeField]
        private PlayerEffectType[] _playerEffects;

        public Sprite Sprite { get => _sprite; set => _sprite = value; }
        public float HeartContainersAdded { get => _heartContainersAdded; set => _heartContainersAdded = value; }
        public float HealthAdded { get => _healthAdded; set => _healthAdded = value; }
        public float TearsAdded { get => _tearsAdded; set => _tearsAdded = value; }
        public float ShotSpeedAdded { get => _shotSpeedAdded; set => _shotSpeedAdded = value; }
        public float SpeedAdded { get => _speedAdded; set => _speedAdded = value; }
        public float TearScaleAdded { get => _tearScaleAdded; set => _tearScaleAdded = value; }
        public float RangeAdded { get => _rangeAdded; set => _rangeAdded = value; }
        public float DamageAdded { get => _damageAdded; set => _damageAdded = value; }
        public float DamageMultiplierAdded { get => _damageMultiplierAdded; set => _damageMultiplierAdded = value; }
    }
}