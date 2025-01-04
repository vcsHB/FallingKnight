using UnityEngine;

namespace UIManage
{

    public class CanvasPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private UIPanel[] _panels;
        public void Open()
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].Open();
            }
        }
        public void Close()
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].Close();
            }
        }

    }

}