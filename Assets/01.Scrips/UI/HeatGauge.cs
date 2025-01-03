using Agents.Players;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.InGame
{

    public class HeatGauge : MonoBehaviour
    {
        
        [SerializeField] private PlayerHeatController _owner;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillDuration = 0.1f;

        private void Awake()
        {
            _owner.OnHeatChangedEvent += HandleHealthChanged;
        }

        public void HandleHealthChanged(float current, float max)
        {
            _fillImage.DOFillAmount(current / max, _fillDuration);
        }
    }

}