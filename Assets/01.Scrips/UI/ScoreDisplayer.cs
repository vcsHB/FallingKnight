using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace UIManage.InGame
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _impactScale = 0.2f;
        [SerializeField] private float _impactDuration = 0.1f;
        public void HandleRefreshScoreText(int deepLevel)
        {
            _text.text = $"{deepLevel.ToString("")}M";
            _text.transform.localScale = new Vector3(_impactScale, _impactScale, 1);
            _text.transform.DOScale(Vector3.one, _impactDuration);
        }
    }

}