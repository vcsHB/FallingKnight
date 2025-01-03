using System.Collections;
using Agents.Players;
using InputSystem;
using UIManage.InGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private StoneCollector _stoneCollector;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private float _waitTerm;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private UIInputReader _uiInputReader;
        //[SerializeField] private FadePanel

        private void Awake()
        {
            SetInputControl(false);
            StartCoroutine(GameStartCoroutine());
        }

        private IEnumerator GameStartCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            SetInputControl(true);
        }


        public void GameOver()
        {
            Time.timeScale = 0f;
            SetInputControl(false);
            _gameOverPanel.Open();
        }

        public void BackToLobby()
        {
            StartCoroutine(BackToLobbyCoroutine());
        }

        private IEnumerator BackToLobbyCoroutine()
        {
            // 페이드 효과
            yield return new WaitForSeconds(_waitTerm);
            Time.timeScale = 1f;
            SetInputControl(true);
            SceneManager.LoadScene("LobbyScene"); // 씬이름 안맞으면 바꾸기
        }

        public void SetInputControl(bool value)
        {
            _playerInput.canControl = value;
            _uiInputReader.canControl = value;
        }



    }

}