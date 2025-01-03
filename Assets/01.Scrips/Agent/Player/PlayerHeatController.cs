using System;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerHeatController : MonoBehaviour, IAgentComponent
    {
        public event Action<float, float> OnHeatChangedEvent;
        [SerializeField] private float _currentHeat;
        [SerializeField] private float _maxHeat = 10f;
        [SerializeField] private float _coolingSpeed = 2f;
        [SerializeField] private float _nonCoolTerm = 2f;
        [SerializeField] private float _dropAttackNeedHeat = 0.7f;
        private float _currentNonCooltime = 0f;

        public bool CanDropAttack => _currentHeat / _maxHeat > _dropAttackNeedHeat;

        private void Update()
        {
            _currentNonCooltime += Time.deltaTime;

            if (_currentNonCooltime > _nonCoolTerm)
            {
                _currentHeat -= _coolingSpeed * Time.deltaTime;
                InvokeHeatChanged();
            }
        }

        public void ResetHeat()
        {
            _currentHeat = 0f;
            InvokeHeatChanged();
        }
        public void GainHeat()
        {
            GainHeat(Time.deltaTime);
        }

        public void GainHeat(float amount)
        { // send DeltaTime
            _currentNonCooltime = 0f;
            ClampHeat();
            _currentHeat += amount;
            InvokeHeatChanged();
        }

        private void InvokeHeatChanged()
        {
            OnHeatChangedEvent?.Invoke(_currentHeat, _maxHeat);

        }

        private void ClampHeat()
        {
            _currentHeat = Mathf.Clamp(_currentHeat, 0, _maxHeat);
        }

        public void Initialize(Agent agent)
        {
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}