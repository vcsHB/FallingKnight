using System.Collections;
using Agents.Players;
using InputSystem;
using Managers.Jsonmanager;
using UIManage.InGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private StoneCollector _stoneCollector;
        [SerializeField] private ScoreManager _scoreManager;
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

            int stoneAmount = _stoneCollector.CollectedAmount;
            int score = _scoreManager.CurrentScore;
            JsonManager.instance.AddMoney(stoneAmount);
            bool isNewScore = JsonManager.instance.gameData.bestSocre < score;
            if(isNewScore)
            {
                JsonManager.instance.gameData.bestSocre = score;
            }
            JsonManager.instance.Save();

            _gameOverPanel.Initialize(stoneAmount, score, isNewScore);
            _gameOverPanel.Open();
        }

        public void BackToLobby()
        {
            StartCoroutine(BackToLobbyCoroutine());
        }

        private IEnumerator BackToLobbyCoroutine()
        {
            // 페이드 효과
            Time.timeScale = 1f;
            yield return new WaitForSeconds(_waitTerm);
            SetInputControl(true);
            SceneManager.LoadScene("TitleScene"); // 씬이름 안맞으면 바꾸기
        }

        public void SetInputControl(bool value)
        {
            _playerInput.canControl = value;
            _uiInputReader.canControl = value;
        }



    }

}