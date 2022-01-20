using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IsaacClone
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pnlMenu;
        [SerializeField]
        private Button _btnContinue;
        [SerializeField]
        private Button _btnRestart;
        [SerializeField]
        private Button _btnExit;
        // Update is called once per frame
        private void Start()
        {
            _btnContinue.onClick.AddListener(() => ExitMenu()) ;
            _btnRestart.onClick.AddListener(() => RestartGame());
            _btnExit.onClick.AddListener(() => ExitGame());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_pnlMenu.activeInHierarchy == true)
                {
                    ExitMenu();
                }
                else
                {
                    _pnlMenu.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }

        public void ExitMenu()
        {
            _pnlMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("SampleScene");
        }

        public void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}