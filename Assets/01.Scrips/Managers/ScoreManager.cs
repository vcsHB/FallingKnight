using System;
using UIManage.InGame;
using UnityEngine;

namespace Managers
{

    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int _currentScore;
        [SerializeField] private Transform _playerTrm;
        [SerializeField] private float _scoreUpdateTerm = 0.2f;
        [SerializeField] private ScoreDisplayer _scoreDisplayer;
        private float _currentCountTime;
        public int CurrentScore => _currentScore;

        private void Update()
        {
            _currentCountTime += Time.deltaTime;    
            if(_currentCountTime > _scoreUpdateTerm)
            {
                _currentCountTime = 0;
                HandleCheckScore();
            }
        }

        private void HandleCheckScore()
        {
            int currentYPos = (int)Mathf.Abs(_playerTrm.position.y);
            if(currentYPos > _currentScore)
            {
                _currentScore = currentYPos;
                _scoreDisplayer.HandleRefreshScoreText(_currentScore);
            }
        }
    }

}