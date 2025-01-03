using System.Collections;
using Agents.Players;
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
        //[SerializeField] private FadePanel


        public void GameOver()
        {
            
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
            SceneManager.LoadScene("LobbyScene"); // 씬이름 안맞으면 바꾸기
        }



    }

}