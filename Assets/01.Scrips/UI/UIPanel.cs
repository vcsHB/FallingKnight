using DG.Tweening;
using UnityEngine;
namespace UIManage
{

    public class UIPanel : MonoBehaviour, IWindowPanel
    {

        [SerializeField] protected bool _useUnscaledTime;
        [SerializeField] protected float _duration;
        protected CanvasGroup _canvasGroup;
        [SerializeField] protected bool _isActive;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public virtual void Close()
        {
            SetActiveCanvasGroup(false);
        }

        public virtual void Open()
        {
            SetActiveCanvasGroup(true);
        }

        public void SetActiveCanvasGroup(bool value)
        {
            _canvasGroup.DOFade(value ? 1f : 0f, _duration).SetUpdate(_useUnscaledTime).Complete(_isActive = value);

            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}