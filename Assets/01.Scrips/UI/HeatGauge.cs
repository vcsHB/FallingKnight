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
        [SerializeField] private Image _gaugeEdgeImage;
        [SerializeField] private float _fillDuration = 0.1f;
        [SerializeField] private ParticleSystem _heatUpVFX;
        [SerializeField] private Color _heatStartColor;
        [SerializeField] private Color _heatEndColor;

        private void Awake()
        {
            _owner.OnHeatChangedEvent += HandleHealthChanged;
        }

        public void HandleHealthChanged(float current, float max)
        {
            float ratio = current / max;
            if (ratio > 0.8f)
                _heatUpVFX.Play();
            else
                _heatUpVFX.Stop();
            _gaugeEdgeImage.color = Color.Lerp(_heatStartColor, _heatEndColor, ratio);
            _fillImage.DOFillAmount(current / max, _fillDuration);

        }
    }

}