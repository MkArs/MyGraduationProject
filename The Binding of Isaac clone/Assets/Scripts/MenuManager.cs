using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IsaacClone
{
    /// <summary>
    /// Отвечает за меню
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pnlMenu;
        [SerializeField]
        private GameObject _pnlItemDescription;
        [SerializeField]
        private Button _btnContinue;
        [SerializeField]
        private Button _btnRestart;
        [SerializeField]
        private Button _btnExit;
        [SerializeField]
        private Text _txtGameResult;
        [SerializeField]
        private Text _txtItemDescription;
        [SerializeField]
        private Text _txtItemName;
        // Update is called once per frame
        private void Start()
        {
            _btnContinue.onClick.AddListener(() => OpenOrExitMenu(false, 1f, ""));
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
        
        /// <summary>
        /// Зайти в меню или выйти из него
        /// </summary>
        /// <param name="isOpening">Открывается ли меню</param>
        /// <param name="timeScale">Прирост времени</param>
        /// <param name="messege">Сообщение игроку</param>
        public void OpenOrExitMenu(bool isOpening, float timeScale, string messege)
        {
            _pnlMenu.SetActive(isOpening);
            Time.timeScale = timeScale;

            if (messege != "" && _btnContinue != null)
            {
                Destroy(_btnContinue.gameObject);
                _txtGameResult.gameObject.SetActive(true);
                _txtGameResult.text = messege;
            }
        }

        /// <summary>
        /// Перезапустить игру
        /// </summary>
        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }

        /// <summary>
        /// Выйти из игры
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        /// <summary>
        /// Показать описание предмета
        /// </summary>
        /// <param name="itemName">Имя предмета</param>
        /// <param name="itemDescription">Описание предмета</param>
        /// <returns></returns>
        public IEnumerator ShowItemDescription(string itemName, string itemDescription)
        {
            _txtItemName.text = itemName;
            _txtItemDescription.text = itemDescription;

            _pnlItemDescription.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            _pnlItemDescription.SetActive(false);
        }

        /// <summary>
        /// Показать описание предмета на случай удаления объекта
        /// </summary>
        /// <param name="itemName">Имя предмета</param>
        /// <param name="itemDescription">Описание предмета</param>
        public void ShowItemDescriptionTrigger(string itemName, string itemDescription)
        {
            StartCoroutine(ShowItemDescription(itemName, itemDescription));
        }
    }
}