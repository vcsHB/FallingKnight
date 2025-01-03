
using InputSystem;
using UnityEngine;
using UnityEngine.Events;
namespace UIManage.InGame
{

    public class PausePanel : UIPanel
    {
        public UnityEvent OnPausePanelCloseEvent;
        [SerializeField] private UIInputReader _uiInput;

        protected override void Awake()
        {
            base.Awake();
            _uiInput.OnEscEvent += TogglePausePanel;
        }

        private void OnDestroy()
        {
            _uiInput.OnEscEvent -= TogglePausePanel;

        }

        public void TogglePausePanel()
        {
            if (_isActive)
                Close();
            else
                Open();
        }
        public override void Open()
        {
            base.Open();
            Time.timeScale = 0f;
        }

        public override void Close()
        {
            base.Close();
            OnPausePanelCloseEvent?.Invoke();
            Time.timeScale = 1f;
        }
    }
}