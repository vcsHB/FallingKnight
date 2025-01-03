using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.InGame
{

    public class StageChangePanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _waitingInterval = 0.2f;
        [SerializeField] private float _duration = 0.2f;
        private Sequence _seq;
        public void Close()
        {
        }
        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _seq = DOTween.Sequence();
            _fadeImage.fillOrigin = 0;
            _seq.Append( _fadeImage.DOFillAmount(1f, _duration).OnComplete(() => {
                _fadeImage.fillOrigin = 1;
            }));
            _seq.AppendInterval(_waitingInterval);
            _seq.Append( _fadeImage.DOFillAmount(0f, _duration));
        }
    }

}