using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Отвечает за отдельную комнату
    /// </summary>
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _workingDoors;
        [SerializeField]
        private GameObject[] _spawnPoints;
        [SerializeField]
        private GameObject[] _objectsToSpawn;

        private GameObject[] _spawnedObjects;
        private bool _isRoomCleared = false;

        public bool IsRoomCleared { get => _isRoomCleared; set => _isRoomCleared = value; }

        /// <summary>
        /// Заспавнить врагов
        /// </summary>
        public void SpawnEnemies()
        {
            if (_isRoomCleared) return;

            short counter = 0;

            Camera.main.GetComponent<SoundManager>().PlayDoorClose();

            CloseOrOpenDoors(true);

            foreach (var prefab in _objectsToSpawn)
            {
                GameObject enemy = Instantiate(prefab, _spawnPoints[counter].transform.position, _spawnPoints[counter].transform.rotation) as GameObject;
                _spawnedObjects[counter] = enemy;
                counter++;
            }

            StartCoroutine(CountEnemies());
        }

        /// <summary>
        /// Открыть или закрыть двери
        /// </summary>
        /// <param name="isDoorClosing">Закрывается ли дверь</param>
        public void CloseOrOpenDoors(bool isDoorClosing)
        {
            if (isDoorClosing)
            {
                foreach (var door in _workingDoors)
                {
                    door.GetComponent<DoorComponent>().OpenOrCloseDoor(isDoorClosing);
                    door.SetActive(false);
                }
            }
            else
            {
                foreach (var door in _workingDoors)
                {
                    door.SetActive(true);
                    door.GetComponent<DoorComponent>().OpenOrCloseDoor(isDoorClosing);
                }
            }
        }

        private void Start()
        {
            _spawnedObjects = new GameObject[_objectsToSpawn.Length];
        }

        /// <summary>
        /// Сосчитать врагов
        /// </summary>
        /// <returns></returns>
        IEnumerator CountEnemies()
        {
            bool areEnemiesDead = false;

            while (areEnemiesDead == false)
            {
                foreach (var enemy in _spawnedObjects)
                {
                    areEnemiesDead = true;

                    if (enemy != null)
                    {
                        areEnemiesDead = false;
                        break;
                    }
                }

                yield return 0;
            }

            Camera.main.GetComponent<SoundManager>().PlayDoorOpen();

            CloseOrOpenDoors(false);

            _isRoomCleared = true;

            var iterations = GameObject.Find("Player").GetComponent<PlayerController>().Luck + 1;

            for (int i = 0; i < iterations; i++)
            {
                Camera.main.GetComponent<PickupRandomizer>().GiveRandomPickup();
            }
        }

        /// <summary>
        /// Заспавнить предмет
        /// </summary>
        public void SpawnItem()
        {
            if (_isRoomCleared) return;

            short counter = 0;

            foreach (var prefab in _objectsToSpawn)
            {
                GameObject item = Instantiate(prefab, _spawnPoints[counter].transform.position, _spawnPoints[counter].transform.rotation) as GameObject;
                counter++;
            }

            _isRoomCleared = true;
        }
    }
}