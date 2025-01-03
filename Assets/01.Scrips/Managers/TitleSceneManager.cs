using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UIManage;

namespace Managers
{

    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField] private float _moveToGameSceneTerm = 1f;
        [Header("Start Animation")]
        [SerializeField] private UIPanel _fadePanel;
        [SerializeField] private RectTransform _playerPanelTrm;
        [SerializeField] private int _jumpNum = 1;
        [SerializeField] private RectTransform _targetPosTrm;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _duration;
        public void HandleGameStart()
        {
            _playerPanelTrm.DOJumpAnchorPos(_targetPosTrm.anchoredPosition, _jumpPower, _jumpNum, _duration);
            StartCoroutine(MoveToGameSceneCoroutine());
        }

        private IEnumerator MoveToGameSceneCoroutine()
        {
            WaitForSeconds ws = new WaitForSeconds(_moveToGameSceneTerm / 2);
            yield return ws;
            _fadePanel.Open();
            yield return ws;
            SceneManager.LoadScene("VCS_Scene");
        }


        public void HandleGameQuit()
        {
            Application.Quit();
        }
    }

}