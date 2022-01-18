using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    public class PickupRandomizer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _penny;
        [SerializeField]
        private GameObject _nickel;
        [SerializeField]
        private GameObject _dime;
        [SerializeField]
        private GameObject _luckyPenny;
        [SerializeField]
        private GameObject _heart;
        [SerializeField]
        private GameObject _bomb;
        [SerializeField]
        private GameObject _key;

        public void GiveRandomPickup()
        {
            var randomNumber = Random.Range(0, 100);

            if (randomNumber < 25)
            {
                GameObject heart = Instantiate(_heart, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
            }
            else if (randomNumber < 50)  
            {
                GameObject bomb = Instantiate(_bomb, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
            }
            else if (randomNumber < 75)
            {
                GameObject key = Instantiate(_key, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
            }
            else
            {
                var randomCoinNumber = Random.Range(0, 100);

                if (randomNumber < 75)
                {
                    GameObject penny = Instantiate(_penny, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
                }
                else if (randomNumber < 95)
                {
                    GameObject nickel = Instantiate(_nickel, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
                }
                else if (randomNumber < 99)
                {
                    GameObject dime = Instantiate(_dime, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
                }
                else
                {
                    GameObject luckyPenny = Instantiate(_luckyPenny, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);
                }
            }
        }
    }
}