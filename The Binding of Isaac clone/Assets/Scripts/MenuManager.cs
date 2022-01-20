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
        [SerializeField]
        private Text _txtGameResult;
        // Update is called once per frame
        private void Start()
        {
            _btnContinue.onClick.AddListener(() => OpenOrExitMenu(false, 1f, "")) ;
            _btnRestart.onClick.AddListener(() => RestartGame());
            _btnExit.onClick.AddListener(() => ExitGame());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameObject.Find("Player").GetComponent<PlayerController>().Health == 0f) return;

                if (_pnlMenu.activeInHierarchy == true)
                {
                    OpenOrExitMenu(false, 1f, "");
                }
                else
                {
                    OpenOrExitMenu(true, 0f, "");
                }
            }
        }

        public void OpenOrExitMenu(bool isOpening, float timeScale, string messege)
        {
            _pnlMenu.SetActive(isOpening);
            Time.timeScale = timeScale;

            if(messege != "" && _btnContinue != null)
            {
                Destroy(_btnContinue.gameObject);
                _txtGameResult.gameObject.SetActive(true);
                _txtGameResult.text = messege;
            }
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