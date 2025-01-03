using Agents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.InGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _owner;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillDuration = 0.1f;

        private void Awake()
        {
            _owner.OnHealthChanged += HandleHealthChanged;
        }

        public void HandleHealthChanged(float current, float max)
        {
            _fillImage.DOFillAmount(current / max, _fillDuration);
        }
    }

}