using System;
using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnDieEvent;
        public UnityEvent OnHealthDecreaseEvent;
        public event Action<float, float> OnHealthChanged;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        private float _currentHealth;
        private float _maxHealth;

        #region External Functions

        public void Initialize(float maxHealth)
        {
            _maxHealth = maxHealth;
            FillHealthMax();
        }

        public void FillHealthMax()
        {
            _currentHealth = _maxHealth;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            ClampHealth();
            OnHealthDecreaseEvent?.Invoke();
            InvokeHealthChanged();
            CheckDie();
        }
        public void RestoreHealth(float amount)
        {
            _currentHealth += amount;
            ClampHealth();
            InvokeHealthChanged();
        }


        #endregion

        private void ClampHealth()
        {
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
        private void CheckDie()
        {
            if (_currentHealth <= 0)
            {
                OnDieEvent?.Invoke();
            }
        }

        private void InvokeHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        }

    }
}