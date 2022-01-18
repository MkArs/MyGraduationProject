using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _workingDoors;
        [SerializeField]
        private GameObject[] _spawnPoints;
        [SerializeField]
        private GameObject[] _enemies;

        private GameObject[] _spawnedEnemies;
        private bool _isRoomCleared = false;

        //public GameObject[] SpawnedEnemies {
        //    get
        //    {
        //        return _spawnedEnemies;
        //    }
        //    set
        //    {
        //        if (value.Length == 0)
        //        {
        //            CloseOrOpenDoors(false);
        //            return;
        //        }
        //    }
        //}

        public void OpenDoors()
        {
            foreach (var door in _workingDoors)
            {
                door.SetActive(true);
            }
        }

        public void SpawnEnemies()
        {
            if (_isRoomCleared) return;

            short counter = 0;

            CloseOrOpenDoors(true);

            foreach (var prefab in _enemies)
            {
                GameObject enemy = Instantiate(prefab, _spawnPoints[counter].transform.position, _spawnPoints[counter].transform.rotation) as GameObject;
                _spawnedEnemies[counter] = enemy;
                counter++;
            }

            StartCoroutine(CountEnemies());
        }

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
            _spawnedEnemies = new GameObject[_enemies.Length];
        }

        IEnumerator CountEnemies()
        {
            bool areEnemiesDead = false;

            while (areEnemiesDead == false)
            {
                foreach (var enemy in _spawnedEnemies)
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

            CloseOrOpenDoors(false);

            _isRoomCleared = true;

            Camera.main.GetComponent<PickupRandomizer>().GiveRandomPickup();
        }
    }
}